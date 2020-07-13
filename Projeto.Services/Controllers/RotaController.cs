using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using Projeto.Data.Repository;
using Projeto.Services.Models.Rota;

namespace Projeto.Services.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class RotaController : ControllerBase
    {
        //atributo
        private readonly IRotaRepository rotaRepository;
        private readonly IClienteRepository clienteRepository;
        private readonly IMapper mapper;

        public RotaController(IRotaRepository rotaRepository, IClienteRepository clienteRepository, IMapper mapper)
        {
            this.rotaRepository = rotaRepository;
            this.clienteRepository = clienteRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post(RotaCadastroModel model)
        {
            //verificando se os campos da model passaram nas validações
            if (ModelState.IsValid)
            {
                try
                {
                    var rota = mapper.Map<Rota>(model);
                    rotaRepository.Inserir(rota);

                    var result = new
                    {
                        message = "Rota cadastrada com sucesso",
                        rota
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

        [HttpPut]
        public IActionResult Put(RotaEdicaoModel model)
        {
            //verificando se os campos da model passaram nas validações
            if (ModelState.IsValid)
            {
                try
                {
                    var rota = mapper.Map<Rota>(model);
                    rotaRepository.Alterar(rota);

                    var result = new
                    {
                        message = "Rota atualizado com sucesso",
                        rota
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
                //buscar o Rota referente ao id informado..
                var rota = rotaRepository.ObterPorId(id);
                var cliente = clienteRepository.Consultar().FirstOrDefault(r=> r.Cod_Rota == id);

                if (cliente != null)
                {
                    return StatusCode(403,$"A Rota não pode ser excluída, pois possui uma Associação com o cliente {cliente.NomeCompleto_RazaoSocial}. ");
                }

                //verificar se o Rota foi encontrado..
                if (rota != null)
                {
                    //excluindo o Rota
                    rotaRepository.Excluir(rota);

                    var result = new
                    {
                        message = "Estoque excluído com sucesso.",
                        rota
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
        [Produces(typeof(List<RotaConsultaModel>))]
        public IActionResult GetAll()
        {
            try
            {
                var result = rotaRepository.Consultar();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(RotaConsultaModel))]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = rotaRepository.ObterPorId(id);

                if (result != null) //se o Rota foi encontrado..
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
    }
}
