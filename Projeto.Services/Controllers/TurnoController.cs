using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Controllers
{
    [Authorize("Bearer")]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoController : ApiControllerBase
    {
        [HttpGet("turnos")]
        public IActionResult ObterTurnos([FromServices] ITurnoRepository turnoRepository)
        {
            var resultado = turnoRepository.ObterTurnos();
            return Result(resultado);
        }

    }
}
