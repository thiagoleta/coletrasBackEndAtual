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
using Projeto.Services.Models.MesReferencia;

namespace Projeto.Services.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class MesReferenciaController : ControllerBase
    {
        //atributo
        private readonly IMesReferenciaRepository mesreferenciaRepository;
        private readonly IMapper mapper;

        public MesReferenciaController(IMesReferenciaRepository mesreferenciaRepository, IMapper mapper)
        {
            this.mesreferenciaRepository = mesreferenciaRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post(MesReferenciaCadastroModel model)
        {
            //verificando se os campos da model passaram nas ões
            if (ModelState.IsValid)
            {
                try
                {
                    var mesreferencia = mapper.Map<MesReferencia>(model);
                    mesreferenciaRepository.Inserir(mesreferencia);

                    var result = new
                    {
                        message = "MesReferencia cadastrada com sucesso", 
                        	mesreferencia
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
        public IActionResult Put(MesReferenciaEdicaoModel model)
        {
            //verificando se os campos da model passaram nas ões
            if (ModelState.IsValid)
            {
                try
                {
                    var mesreferencia = mapper.Map<MesReferencia>(model);
                    mesreferenciaRepository.Alterar(mesreferencia);

                    var result = new
                    {
                        message = "MesReferencia atualizado com sucesso",
                        	mesreferencia
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
                //buscar o MesReferencia referente ao id informado..
                var mesreferencia = mesreferenciaRepository.ObterPorId(id);

                //verificar se o MesReferencia foi encontrado..
                if (mesreferencia != null)
                {
                    //excluindo o MesReferencia
                    mesreferenciaRepository.Excluir(mesreferencia);

                    var result = new
                    {
                        message = "MesReferencia excluído com sucesso.",
                        	mesreferencia
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
        [Produces(typeof(List<MesReferenciaConsultaModel>))]
        public IActionResult GetAll()
        {
            try
            {
                var result = mesreferenciaRepository.Consultar();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(MesReferenciaConsultaModel))]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = mesreferenciaRepository.ObterPorId(id);

                if (result != null) //se o MesReferencia foi encontrado..
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
