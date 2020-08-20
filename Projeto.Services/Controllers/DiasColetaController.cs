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
using Projeto.Services.Models.DiasSemana;

namespace Projeto.Services.Controllers
{
    [Authorize("Bearer")]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class DiasColetaController : ControllerBase
    {
        private readonly IDiasColetaRepository diasColetaRepository;
        private readonly IMapper mapper;

        public DiasColetaController(IDiasColetaRepository diasColetaRepository, IMapper mapper)
        {
            this.diasColetaRepository = diasColetaRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Produces(typeof(List<DiaColetaConsultaModel>))]
        public IActionResult GetAll()
        {
            try
            {
                var result = diasColetaRepository.Consultar();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }
    }
}
