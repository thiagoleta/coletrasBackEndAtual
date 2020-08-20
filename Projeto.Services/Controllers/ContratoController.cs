using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using Projeto.Data.Repository;
using Projeto.Services.Models.Contrato;

namespace Projeto.Services.Controllers
{
    [Authorize("Bearer")]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoController : ControllerBase
    {
        //atributo
        private readonly IContratoRepository contratoRepository;
        private readonly IMapper mapper;

        public ContratoController(IContratoRepository contratoRepository, IMapper mapper)
        {
            this.contratoRepository = contratoRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post(ContratoCadastroModel model)
        {
            //verificando se os campos da model passaram nas validações
            if (ModelState.IsValid)
            {
                try
                {
                    var contrato = mapper.Map<Contrato>(model);
                    contratoRepository.Inserir(contrato);

                    var result = new
                    {
                        message = "Contrato cadastrado com sucesso", 
                        	contrato
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
        public IActionResult Put(ContratoEdicaoModel model)
        {
            //verificando se os campos da model passaram nas validações
            if (ModelState.IsValid)
            {
                try
                {
                    var contrato = mapper.Map<Contrato>(model);
                    contratoRepository.Alterar(contrato);

                    var result = new
                    {
                        message = "Contrato atualizado com sucesso",
                        	contrato
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
                //buscar o Contrato referente ao id informado..
                var contrato = contratoRepository.ObterPorId(id);

                //verificar se o Contrato foi encontrado..
                if (contrato != null)
                {
                    //excluindo o Contrato
                    contratoRepository.Excluir(contrato);

                    var result = new
                    {
                        message = "Contrato excluído com sucesso.",
                        	contrato
                    };

                    return Ok(result);
                }
                else
                {
                    return BadRequest("Contrato não encontrado.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }

        [HttpGet]
        [Produces(typeof(List<ContratoConsultaModel>))]
        public IActionResult GetAll()
        {
            try
            {
                var result = contratoRepository.Consultar();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(ContratoConsultaModel))]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = contratoRepository.ObterPorId(id);

                if (result != null) //se o Contrato foi encontrado..
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
