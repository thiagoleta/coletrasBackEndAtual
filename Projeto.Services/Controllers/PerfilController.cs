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
using Projeto.Services.Models.Perfil;

namespace Projeto.Services.Controllers
{

    [Authorize("Bearer")]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilRepository perfilRepository;
        private readonly IMapper mapper;

        public PerfilController(IPerfilRepository perfilRepository, IMapper mapper)
        {
            this.perfilRepository = perfilRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post(PerfilCadastroModel model)
        {
            //verificando se os campos da model passaram nas ões
            if (ModelState.IsValid)
            {
                try
                {
                    var perfil = mapper.Map<Perfil>(model);
                    perfilRepository.Inserir(perfil);

                    var result = new
                    {
                        message = "Perfil cadastrado com sucesso",
                        perfil
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
        public IActionResult Put(PerfilEdicaoModel model)
        {
            //verificando se os campos da model passaram nas ões
            if (ModelState.IsValid)
            {
                try
                {
                    var perfil = mapper.Map<Perfil>(model);
                    perfilRepository.Alterar(perfil);

                    var result = new
                    {
                        message = "Perfil atualizado com sucesso",
                        perfil
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

        [HttpGet]
        [Produces(typeof(List<PerfilConsultaModel>))]
        public IActionResult GetAll()
        {
            try
            {
                var result = perfilRepository.Consultar();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(PerfilConsultaModel))]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = perfilRepository.ObterPorId(id);

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


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            try
            {
                //buscar o OS referente ao id informado..
                var perfil = perfilRepository.ObterPorId(id);

                //verificar se o OS foi encontrado..
                if (perfil != null)
                {
                    //excluindo o OS
                    perfilRepository.Excluir(perfil);

                    var result = new
                    {
                        message = "Perfil excluído com sucesso.",
                        perfil
                    };

                    return Ok(result);
                }
                else
                {
                    return BadRequest("Perfil não encontrado.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }

    }
}
