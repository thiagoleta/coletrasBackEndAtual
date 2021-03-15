using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;
using Projeto.Data.Commands;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using Projeto.Data.Extensions;
using Projeto.Data.Repository;
using Projeto.Data.Repository.Sorts;
using Projeto.Data.Seedwork;
using Projeto.Data.Services;
using Projeto.Data.ValueObjects;
using Projeto.Services.Models.Motorista;

namespace Projeto.Services.Controllers
{
    //[Authorize("Bearer")]
    [AllowAnonymous]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class MotoristaController : ApiControllerBase
    {     

        [HttpPost]
        public IActionResult Criar(
         [FromServices] IMotoristaService service,
         [FromBody] CriarMotoristaCommand command)
        {
            return Result(service.Criar(command));
        }

      

        [HttpPut]
        public IActionResult Atualizar(
         [FromServices] IMotoristaService service,
         [FromBody] AtualizarMotoristaCommand command)
        {
            return Result(service.Atualizar(command));
        }


        [HttpDelete("{cod_Motorista}")]
        public IActionResult Remover(
     [FromServices] IMotoristaService service,
      [FromRoute] int cod_Motorista)
        {
            return Result(service.Remover(cod_Motorista));
        }    


        [HttpGet]
        public IActionResult ObterPaginado(
         [FromServices] IMotoristaRepository motoristaRepository,
         [FromQuery] int pagina, [FromQuery] int quantidade,
         [FromQuery] string coluna = "nome",
         [FromQuery] string direcao = "asc",
         [FromQuery] string nome = null
         )
        {
            return Result(motoristaRepository.ObterPaginado(pagina, quantidade, EnumHelpers.ParseOrDefault(coluna, MotoristaSort.Nome),
                string.IsNullOrEmpty(direcao) || direcao.Equals("asc"), DataString.FromNullableString(nome)));
        }

        [HttpGet("exportar")]
        public IActionResult Exportar([FromServices] IMotoristaRepository repository,
          [FromQuery] string coluna = "nome", [FromQuery] string direcao = "asc", [FromQuery] string nome = null)
        {
            var resultado = repository.Obter(EnumHelpers.ParseOrDefault(coluna, MotoristaSort.Nome),
                string.IsNullOrEmpty(direcao) || direcao.Equals("asc"), DataString.FromNullableString(nome));

            if (resultado.Tipo == ResultType.Valid)
            {
                StringBuilder csv = new StringBuilder();
                csv.AppendLine("COD_MOTORISTA; NOME; AJUDANTE1; AJUDANTE2; TELEFONE1, TELEFONE2; PLACA");

                foreach (var x in resultado.Dados)
                {

                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Nome) ? x.Nome : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Ajudante1) ? x.Ajudante1 : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Ajudante2) ? x.Ajudante2 : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Telefone1) ? x.Telefone1 : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Telefone2) ? x.Telefone2 : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Placa) ? x.Placa : string.Empty)}\";");
                    csv.AppendLine("");
                }

                string nomeArquivo = $"Motorista{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.csv";
                byte[] bytes = Encoding.UTF8.GetBytes(csv.ToString());
                bytes = Encoding.UTF8.GetPreamble().Concat(bytes).ToArray();
                return File(bytes, "text/csv", nomeArquivo);
            }
            return Result(resultado);
        }

        [HttpGet("motoristas")]
        public IActionResult ObterMotoristas([FromServices] IMotoristaRepository motoristaRepository)
        {
            var resultado = motoristaRepository.ObterMotoristas();
            return Result(resultado);
        }

    }
}

