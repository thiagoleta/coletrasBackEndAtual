using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using Projeto.Data.Repository;
using Projeto.Services.Models.OS;
using Projeto.Services.Util;

namespace Projeto.Services.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class OSController : ControllerBase
    {
        //atributo
        private readonly IOSRepository osRepository;
        private readonly IMesReferenciaRepository mesRepository;
        private readonly IContratoRepository contratoRepository;
        private readonly IClienteRepository clienteRepository;
        private readonly IConfiguracaoRepository configuracaoRepository;
        private readonly Criptografia md5Encrypt; //crosscutting de criptografia
        private readonly MailService mailService;
        private readonly IMapper mapper;

        public OSController(IOSRepository osRepository, IMesReferenciaRepository mesRepository, IContratoRepository contratoRepository, IClienteRepository clienteRepository, IConfiguracaoRepository configuracaoRepository, Criptografia md5Encrypt, MailService mailService, IMapper mapper)
        {
            this.osRepository = osRepository;
            this.mesRepository = mesRepository;
            this.contratoRepository = contratoRepository;
            this.clienteRepository = clienteRepository;
            this.configuracaoRepository = configuracaoRepository;
            this.md5Encrypt = md5Encrypt;
            this.mailService = mailService;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post(OSCadastroModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    if (model.Clientes != null)
                    {
                        foreach (var itens in model.Clientes)
                        {
                            var contratoAtivo = contratoRepository.Consultar()
                            .FirstOrDefault(co => co.Cod_Cliente.Equals(itens.Cod_Cliente) && co.Flag_Termino.Equals(false));

                            if (contratoAtivo != null)
                            {

                                var os = new OS();

                                os.Cod_Contrato = contratoAtivo.Cod_Contrato;
                                os.Cod_Cliente = itens.Cod_Cliente;
                                os.Data_Geracao = DateTime.Now;
                                os.Data_Coleta = null;
                                os.Flag_Ativo = true;
                                os.Flag_Cancelado = false;
                                os.Quantidade_Coletada = 0;
                                os.Flag_Coleta = false;
                                os.Flag_Cancelado = false;
                                os.Motivo_Cancelamento = null;
                                os.Data_Cancelamento = null;
                                

                                var MesRef = mesRepository.Consultar().FirstOrDefault(m => m.Data_Encerramento == null && m.Flag_Encerramento.Equals(false));

                                if (MesRef is null )
                                {
                                    return StatusCode(403, $"Não existe Mês referência aberto para cadastro da OS, favor cadastrar o Mês Referência");
                                }

                                os.Cod_MesReferencia = MesRef.Cod_MesReferencia;
                               

                                var configuracao = configuracaoRepository.Consultar().FirstOrDefault(c=> c.Flag_Ativo.Equals(true));                     

                                if (configuracao is null)
                                {
                                    return StatusCode(403, $"Não existe confuguração padrão ativa para geração da OS.");
                                }
                                os.Cod_Configuracao = configuracao.Cod_Configuracao;
                                osRepository.Inserir(os);                               

                            }
                            else
                            {
                                return StatusCode(403, $"Não existe contrato Ativo para o {itens.Cod_Cliente}.");
                            }
                        }
                    }

                    else
                    {
                        return StatusCode(403, $"Favor selecionar o cliente para geração das OSs. ");
                    }


                    return Ok("OS cadastrada com sucesso");
                }
                catch (Exception e)
                {
                    return StatusCode(500, "Erro: " + e.Message);
                }
            }
            else
            {
                //Erro HTTP 400 (BAD REQUEST)
                return BadRequest("Ocorreram erros de validação.");
            }
        }

        [HttpPut]
        public IActionResult Put(OSEdicaoModel model)
        {
            //verificando se os campos da model passaram nas ões
            if (ModelState.IsValid)
            {
                try
                {
                    var os = mapper.Map<OS>(model);
                    osRepository.Alterar(os);

                    var result = new
                    {
                        message = "OS atualizado com sucesso",
                        os
                    };

                    return Ok(result); //HTTP 200 (SUCESSO!)
                }
                catch (Exception e)
                {
                    return StatusCode(500, "Erro: " + e.Message);
                }
            }
            else
            {
                //Erro HTTP 400 (BAD REQUEST)
                return BadRequest("Ocorreram erros de validação.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            try
            {
                //buscar o OS referente ao id informado..
                var os = osRepository.ObterPorId(id);

                //verificar se o OS foi encontrado..
                if (os != null)
                {
                    //excluindo o OS
                    osRepository.Excluir(os);

                    var result = new
                    {
                        message = "OS excluído com sucesso.",
                        os
                    };

                    return Ok(result);
                }
                else
                {
                    return BadRequest("Estoque não encontrado.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }

        [HttpGet]
        [Produces(typeof(List<OSConsultaModel>))]
        public IActionResult GetAll()
        {
            try
            {
                var result = osRepository.Consultar();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(OSConsultaModel))]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = osRepository.ObterPorId(id);

                if (result != null) //se o OS foi encontrado..
                {
                    return Ok(result);
                }
                else
                {
                    return NoContent(); //HTTP 204 (SUCESSO -> Vazio)
                }
            }

            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }

        private void EnviarEmailDeBoasVindas(Cliente cliente)
        {
            var assunto = "Conta de Usuário criada com sucesso- COTI INFORMÁTICA";
            var texto = new StringBuilder();

            texto.Append($"Olá, {cliente.NomeCompleto_RazaoSocial}\n\n");
            texto.Append($"Sua conta de usuário foi criada com sucesso!\n");
            texto.Append($"Faça seu login para ter acesso ao sistema.");
            texto.Append($"\n\n");
            texto.Append($"Atenciosamente,\n");
            texto.Append($"Equipe COTI Informática");

            mailService.SendMail(cliente.Email, assunto, texto.ToString());
        }

    }
}
