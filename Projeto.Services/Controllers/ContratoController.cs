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
using Projeto.Data.Commands;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using Projeto.Data.Extensions;
using Projeto.Data.Repository;
using Projeto.Data.Repository.Sorts;
using Projeto.Data.Seedwork;
using Projeto.Data.Services;
using Projeto.Services.Models.Contrato;

namespace Projeto.Services.Controllers
{
    //[Authorize("Bearer")]
    [AllowAnonymous]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]

    public class ContratoController : ApiControllerBase
    {

        [HttpPost]
        public IActionResult Criar([FromServices] IContratoService service, [FromBody] CriarContratoCommand command)
        {
            return Result(service.Criar(command));
        }

        [HttpPut]
        public IActionResult Atualizar(
               [FromServices] IContratoService service,
               [FromBody] AtualizarContratoCommand command)
        {
            return Result(service.Atualizar(command));
        }


        [HttpDelete("{cod_Contrato}")]
        public IActionResult Remover(
        [FromServices] IContratoService service,
         [FromRoute] int cod_Contrato)
        {
            return Result(service.Remover(cod_Contrato));
        }

        [HttpGet]
        public IActionResult ObterPaginado([FromServices] IContratoRepository repository,

         [FromQuery] int pagina = 1,
             [FromQuery] int quantidade = 8,
             [FromQuery] string coluna = "cliente", [FromQuery] string direcao = "asc")
        {
            return Result(repository.ObterPaginado(pagina,quantidade, EnumHelpers.ParseOrDefault(coluna, ContratoSort.ClienteNome),
                string.IsNullOrEmpty(direcao) || direcao.Equals("asc")));
        }



        [HttpGet("exportar")]
        public IActionResult Exportar([FromServices] IContratoRepository repository,
                                     [FromQuery] string coluna = "nome",
                                     [FromQuery] string direcao = "asc")
        {
            var resultado = repository.Obter(EnumHelpers.ParseOrDefault(coluna, ContratoSort.ClienteNome),
                string.IsNullOrEmpty(direcao) || direcao.Equals("asc"));

            if (resultado.Tipo == ResultType.Valid)
            {
                StringBuilder csv = new StringBuilder();
                csv.AppendLine("COD_CLIENTE; NOME CLIENTE;COLETA CONTRATADA; VALOR LIMITE; VALOR UNIDADE;MOTIVO CANCELAMENTO;DATA CANCELAMENTO;FLAG TERMINO; DATA INICIO; DATA TERMINO  CONTRATO  ");

                foreach (var x in resultado.Dados)
                {
                    csv.Append($"\"{x.Cod_Contrato}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Cliente.NomeCompleto_RazaoSocial) ? x.Cliente.NomeCompleto_RazaoSocial : string.Empty)}\";");
                    csv.Append($"=\"{x.ColetaContratada}\";");
                    csv.Append($"=\"{x.ValorLimite}\";");
                    csv.Append($"=\"{x.ValorUnidade}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.MotivoCancelamento) ? x.MotivoCancelamento : string.Empty)}\";");
                    csv.Append($"=\"{x.DataCancelamento}\";");
                    csv.Append($"\"{(x.FlagTermino != null ? (Convert.ToBoolean(x.FlagTermino) ? "Sim" : "Não") : "Não")}\";");
                    csv.Append($"=\"{x.DataTermino}\";");
                    csv.AppendLine("");                    
                }

        string nomeArquivo = $"Contrato{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.csv";
                byte[] bytes = Encoding.UTF8.GetBytes(csv.ToString());
                bytes = Encoding.UTF8.GetPreamble().Concat(bytes).ToArray();
                return File(bytes, "text/csv", nomeArquivo);
            }
            return Result(resultado);
        }

    }
}
