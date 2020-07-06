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
using Projeto.Services.Models.OS;

namespace Projeto.Services.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class OSController : ControllerBase
    {
        //atributo
        private readonly IOSRepository osRepository;
        private readonly IMapper mapper;

        public OSController(IOSRepository osRepository, IMapper mapper)
        {
            this.osRepository = osRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post(OSCadastroModel model)
        {
            //verificando se os campos da model passaram nas ões
            if (ModelState.IsValid)
            {
                try
                {
                    var os = mapper.Map<OS>(model);
                    osRepository.Inserir(os);

                    var result = new
                    {
                        message = "OS cadastrada com sucesso", 
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
    }
}
