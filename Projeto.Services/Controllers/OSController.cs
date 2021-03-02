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
using Microsoft.CodeAnalysis;
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
                        foreach (var cliente in model.Clientes)
                        {
                            var contratoAtivo = contratoRepository.Consultar()
                            .FirstOrDefault(co => co.CodCliente.Equals(cliente.Cod_Cliente) && co.FlagTermino.Equals(false));

                            if (contratoAtivo != null)
                            {

                                var os = new OS();                                
                                os.Endereco_Cliente = cliente.Endereco;
                                os.NomeCompleto_RazaoSocial_Cliente = cliente.NomeCompleto_RazaoSocial;
                                os.Email_Cliente = cliente.Email;
                                os.Cod_Contrato = contratoAtivo.Cod_Contrato;
                                os.Valor_Limite = contratoAtivo.ValorLimite;
                                os.Coleta_Contratada = contratoAtivo.ColetaContratada;
                                os.Valor_Unidade = contratoAtivo.ValorUnidade;
                                os.Cod_Cliente = cliente.Cod_Cliente;

                                System.Globalization.CultureInfo brasil = new System.Globalization.CultureInfo("pt-BR");
                                String dataBr = DateTime.Now.ToString(brasil);     
                                var dt = DateTime.Parse(dataBr);                            

                                os.Data_Geracao = dt;

                                var MesRef = mesRepository.Consultar().FirstOrDefault(m => m.DataTermino == null && m.Ativo.Equals(false));

                                if (MesRef is null )
                                {
                                    return StatusCode(403, $"Não existe Mês referência aberto para cadastro da OS, favor cadastrar o Mês Referência");
                                }

                                string nomeMotorista = "thiago";
                                string recebido = "thiago";
                                string material = "thiago";
                                int quatidadeColeta = 2;
                                string email  = "thiagoleta2013@gmail.com";
                                os.Cod_MesReferencia = MesRef.Cod_MesReferencia;
                                osRepository.Inserir(os);
                                EnviarEmailOs( os.NomeCompleto_RazaoSocial_Cliente,
                                                os.Endereco_Cliente,
                                                os.Data_Geracao,
                                                os.Hora_Entrada,
                                                os.Hora_Saida, 
                                                nomeMotorista,
                                                recebido,
                                                material,
                                                 quatidadeColeta, os.Placa, email);

                                //var configuracao = configuracaoRepository.Consultar().FirstOrDefault(c => c.Flag_Ativo.Equals(true));

                                //if (configuracao is null)
                                //{
                                //    return StatusCode(403, $"Não existe confuguração padrão ativa para geração da OS.");
                                //}

                                //os.Empresa_Config = configuracao.Empresa;
                                //var enderecoCompleto = string.Format("{0} - {1} - {2}. ", configuracao.Endereco, configuracao.Bairro, configuracao.Cidade);
                                //os.Endereco_Completo_Config = enderecoCompleto;
                                //var telefones = ($"Telefone : {configuracao.Telefones}");
                                //os.Telefones_Config = telefones;
                                //var CNPJ = ($"CNPJ : {configuracao.CNPJ}");
                                //os.CNPJ_Config = CNPJ;
                                //os.Inscr_Municipal_Config = configuracao.Inscr_Municipal;
                                //os.Numero_Inea_Config = configuracao.Numero_Inea;                              
                                //os.Cod_Configuracao = configuracao.Cod_Configuracao;



                            }
                            else
                            {
                                return StatusCode(403, $"Não existe contrato Ativo para o {cliente.Cod_Cliente}.");
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
                    //var configuracao = configuracaoRepository.Consultar().FirstOrDefault(c => c.Flag_Ativo.Equals(true));

                    //if (configuracao !=null)
                    //{
                    //    result.Empresa_Config = configuracao.Empresa;

                    //    var enderecoCompleto = string.Format("{0} - {1} - {2}. ", configuracao.Endereco, configuracao.Bairro, configuracao.Cidade);

                    //    result.Endereco_Completo_Config = enderecoCompleto;

                    //    var telefones = ($"Telefone : {configuracao.Telefones}");
                    //    result.Telefones_Config = telefones;

                    //    var CNPJ = ($"CNPJ : {configuracao.CNPJ}");
                    //    result.CNPJ_Config = CNPJ;

                    //    result.Inscr_Municipal_Config = configuracao.Inscr_Municipal;
                    //    result.Numero_Inea_Config = configuracao.Numero_Inea;
                    //}

                    //var cliente = clienteRepository.Consultar().FirstOrDefault(c=> c.Cod_Cliente == result.Cod_Cliente);

                    //if (cliente != null)
                    //{
                    //    result.NomeCompleto_RazaoSocial_Cliente = cliente.NomeCompleto_RazaoSocial;
                    //    result.Endereco_Cliente = cliente.Endereco;
                    //}


                    //var gravarConfiguracao = osRepository.Alterar(result);
                    
                    
                    
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

        private void EnviarEmailOs(string NomeCompletoRazaoSocialCliente,
                                                string EnderecoCliente, DateTime DataColeta,
                                                string horaEntrada, string horaSaida, string nomeMotorista,
                                                string recebido, string material, int quatidadeColeta, string placa, string emailCliente)
        {
            var assunto = "Guia de OS - Coletrans";


            StringBuilder corpoemail = new StringBuilder();
            corpoemail.AppendLine("<p><strong><&nbsp; Coletrans</strong></p>");
            corpoemail.AppendLine("<p style='text-align: center;'>&nbsp;</p>");
            corpoemail.AppendLine("<p style='text-align: center;'><strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Rua Ari Barroso, n&ordm;. 294 - Parque Beira Mar - Duque de Caxias - RJ.</strong></p>");
            corpoemail.AppendLine("<p style='text-align: center;'><strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Telefone : (21) 2771-4487 (21) 97576-7024</strong></p>");
            corpoemail.AppendLine("<p style='text-align: center;'>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;CNPJ : 06697682/0002-09- Inscr. Mun 780148-3 LO IN INEA02198</p>");
            corpoemail.AppendLine("<p style='text-align: center;'>&nbsp;&nbsp;</p>");
            corpoemail.AppendLine("<table style='height: 172px; margin-left: auto; margin-right: auto;' border='1' width='620' cellspacing='1'>");
            corpoemail.AppendLine("<tbody>");
            corpoemail.AppendLine("<tr>");
            corpoemail.AppendLine($"<td style='width: 297px;'>&Aacute; servi&ccedil;o de : {NomeCompletoRazaoSocialCliente} </td>");
            corpoemail.AppendLine("<td style='width: 307px;'>&nbsp;</td>");
            corpoemail.AppendLine("</tr>");
            corpoemail.AppendLine("<tr>");
            corpoemail.AppendLine($"<td style='width: 297px;'>Endere&ccedil;o : {EnderecoCliente} </td>");
            corpoemail.AppendLine("<td style='width: 307px;'>&nbsp;</td>");
            corpoemail.AppendLine("</tr>");
            corpoemail.AppendLine("<tr>");
            corpoemail.AppendLine($"<td style='width: 297px;'>Data:&nbsp; {DataColeta}</td>");
            corpoemail.AppendLine($"<td style='width: 307px;'>Placa : {placa}</td>");
            corpoemail.AppendLine("</tr>");
            corpoemail.AppendLine($"<td style='width: 297px;'>Motorista : {nomeMotorista}</td>");
            corpoemail.AppendLine("<td style='width: 307px;'>&nbsp;</td>");
            corpoemail.AppendLine("</tr>");
            corpoemail.AppendLine("<td style='width: 297px;'>Recebido :</td>");
            corpoemail.AppendLine("<td style='width: 307px;'>&nbsp;</td>");
            corpoemail.AppendLine("</tr>");
            corpoemail.AppendLine("<tr>");
            corpoemail.AppendLine($"<td style='width: 297px;'>Material : {material}</td>");
            corpoemail.AppendLine("<td style='width: 307px;'>&nbsp;</td>");
            corpoemail.AppendLine("</tr>");
            corpoemail.AppendLine("<tr>");
            corpoemail.AppendLine($"<td style='width: 297px;'>Quanditade de Coleta : {quatidadeColeta}</td>");
            corpoemail.AppendLine("<td style='width: 307px;'>&nbsp;</td>");
            corpoemail.AppendLine("</tr>");
            corpoemail.AppendLine("</tbody>");
            corpoemail.AppendLine("/table>");

            //string Body = System.IO.File.ReadAllText(@"C:\Users\thiago.leta\Desktop\Nova pasta\OS.html");

            mailService.SendMail(emailCliente, assunto, corpoemail.ToString());
        }

    }
}
