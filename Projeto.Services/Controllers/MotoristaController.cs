using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using Projeto.Data.Repository;
using Projeto.Services.Models.Motorista;

namespace Projeto.Services.Controllers
{
    [Authorize("Bearer")]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class MotoristaController : ControllerBase
    {


        //atributo
        private readonly IMotoristaRepository motoristaRepository;
        private readonly IRotaRepository rotaRepository;
        private readonly IMapper mapper;

        public MotoristaController(IMotoristaRepository motoristaRepository, IRotaRepository rotaRepository, IMapper mapper)
        {
            this.motoristaRepository = motoristaRepository;
            this.rotaRepository = rotaRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post(MotoristaCadastroModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var motorista = mapper.Map<Motorista>(model);
                    motoristaRepository.Inserir(motorista);

                    var result = new
                    {
                        message = "Motorista cadastrado com sucesso.",
                        motorista
                    };

                    return Ok(result);
                }

                catch (Exception e)
                {
                    return StatusCode(500, "Erro: " + e.Message);
                }
            }
            else
            {
                return BadRequest("Ocorreram erros de validação.");
            }
        }

        [HttpPut]
        public IActionResult Put(MotoristaEdicaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var motorista = mapper.Map<Motorista>(model);
                    motoristaRepository.Alterar(motorista);

                    var result = new
                    {
                        message = "Motorista atualizado com sucesso.",
                        motorista
                    };

                    return Ok(result);
                }
                catch (Exception e)
                {
                    return StatusCode(500, "Erro: " + e.Message);
                }
            }
            else
            {
                return BadRequest("Ocorreram erros de validação.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var motorista = motoristaRepository.ObterPorId(id);

                //var rota = rotaRepository.Consultar().FirstOrDefault(m => m.Motorista.Cod_Motorista == id);

                //if (rota != null)
                //{
                //    return StatusCode(403, $"O Motorista {motorista.Nome}  Não pode ser excluído, pois existe uma rota Associada.");

                //}

                if (motorista != null)
                {
                    motoristaRepository.Excluir(motorista);

                    var result = new
                    {
                        message = "Motorista excluído com sucesso.",
                        motorista
                    };

                    return Ok(result);
                }

                else
                {
                    return BadRequest("Motorista não encontrado.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = motoristaRepository.Consultar();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(MotoristaConsultaModel))]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = motoristaRepository.ObterPorId(id);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }
    }
}

