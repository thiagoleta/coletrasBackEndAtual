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
using Projeto.Services.Models.Configucacao;

namespace Projeto.Services.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigucacaoController : ControllerBase
    {
        //atributo
        private readonly IConfigucacaoRepository configucacaoRepository;
        private readonly IMapper mapper;

        public ConfigucacaoController(IConfigucacaoRepository configucacaoRepository, IMapper mapper)
        {
            this.configucacaoRepository = configucacaoRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post(ConfigucacaoCadastroModel model)
        {
            //verificando se os campos da model passaram nas validações
            if (ModelState.IsValid)
            {
                try
                {
                    var configucacao = mapper.Map<Configucacao>(model);
                    configucacaoRepository.Inserir(configucacao);

                    var result = new
                    {
                        message = "Configucacao cadastrada com sucesso", 
                        	configucacao
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
        public IActionResult Put(ConfigucacaoEdicaoModel model)
        {
            //verificando se os campos da model passaram nas validações
            if (ModelState.IsValid)
            {
                try
                {
                    var configucacao = mapper.Map<Configucacao>(model);
                    configucacaoRepository.Alterar(configucacao);

                    var result = new
                    {
                        message = "Configucacao atualizado com sucesso",
                        	configucacao
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
                //buscar o Configucacao referente ao id informado..
                var configucacao = configucacaoRepository.ObterPorId(id);

                //verificar se o Configucacao foi encontrado..
                if (configucacao != null)
                {
                    //excluindo o Configucacao
                    configucacaoRepository.Excluir(configucacao);

                    var result = new
                    {
                        message = "Configucacao excluído com sucesso.",
                        	configucacao
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
        [Produces(typeof(List<ConfigucacaoConsultaModel>))]
        public IActionResult GetAll()
        {
            try
            {
                var result = configucacaoRepository.Consultar();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(ConfigucacaoConsultaModel))]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = configucacaoRepository.ObterPorId(id);

                if (result != null) //se o Configucacao foi encontrado..
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
