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
using Projeto.Services.Models.Roteiro;

namespace Projeto.Services.Controllers
{
    [Authorize("Bearer")]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoteiroController : ControllerBase
    {
        
        private readonly IRoteiroRepository rotreiroRepository;
        private readonly IMapper mapper;

        public RoteiroController(IRoteiroRepository rotreiroRepository, IMapper mapper)
        {
            this.rotreiroRepository = rotreiroRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post(RoteiroCadastroModel model)
        {
            //verificando se os campos da model passaram nas ões
            if (ModelState.IsValid)
            {
                try
                {
                    var roteiro = mapper.Map<Roteiro>(model);
                    rotreiroRepository.Inserir(roteiro);

                    var result = new
                    {
                        message = "Roteiro cadastrado com sucesso",
                        roteiro
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
        public IActionResult Put(RoteiroEdicaoModel model)
        {
            //verificando se os campos da model passaram nas ões
            if (ModelState.IsValid)
            {
                try
                {
                    var roteiro = mapper.Map<Roteiro>(model);
                    rotreiroRepository.Alterar(roteiro);

                    var result = new
                    {
                        message = "Roteiro atualizado com sucesso",
                        roteiro
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
                var roteiro = rotreiroRepository.ObterPorId(id);

                //verificar se o OS foi encontrado..
                if (roteiro != null)
                {
                    //excluindo o OS
                    rotreiroRepository.Excluir(roteiro);

                    var result = new
                    {
                        message = "Roteiro excluído com sucesso.",
                        roteiro
                    };

                    return Ok(result);
                }
                else
                {
                    return BadRequest("Roteiro não encontrado.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }


        [HttpGet("{id}")]
        [Produces(typeof(RoteiroConsultaModel))]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = rotreiroRepository.ObterPorId(id);

                if (result != null) //se o roteiro foi encontrado..
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

        [HttpGet]
        [Produces(typeof(List<RoteiroConsultaModel>))]
        public IActionResult GetAll()
        {
            try
            {
                var result = rotreiroRepository.Consultar();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }
    }
}
