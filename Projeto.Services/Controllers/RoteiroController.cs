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
using Projeto.Data.Repository.Sorts;
using Projeto.Data.Seedwork;
using Projeto.Data.Services;
using Projeto.Services.Models.Roteiro;

namespace Projeto.Services.Controllers
{
    //[Authorize("Bearer")]
    [AllowAnonymous]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoteiroController : ApiControllerBase
    {

        [HttpPost]
        public IActionResult Criar([FromServices] IRoteiroService service, [FromBody] CriarRoteiroCommand command)
        {
            return Result(service.Criar(command));
        }

        [HttpPut]
        public IActionResult Atualizar([FromServices] IRoteiroService service, [FromBody] AtualizarRoteiroCommand command)
        {
            return Result(service.Atualizar(command));
        }

        [HttpDelete("{cod_Roteiro}")]
        public IActionResult Remover(
                              [FromServices] IRoteiroService service,
                              [FromRoute] int cod_Roteiro)
        {
            return Result(service.Remover(cod_Roteiro));
        }


        [HttpGet]
        public IActionResult ObterPaginado([FromServices] IRoteiroRepository repository,

             [FromQuery] int pagina = 1,
             [FromQuery] int quantidade = 8,
             [FromQuery] string coluna = "cliente", [FromQuery] string direcao = "asc")
        {
            return Result(repository.ObterPaginado(pagina, quantidade, EnumHelpers.ParseOrDefault(coluna, RoteiroSort.Cliente),
                string.IsNullOrEmpty(direcao) || direcao.Equals("asc")));
        }


        [HttpGet("exportar")]
        public IActionResult Exportar([FromServices] IRoteiroRepository repository,
                                     [FromQuery] string coluna = "cliente",
                                     [FromQuery] string direcao = "asc")
        {
            var resultado = repository.Obter(EnumHelpers.ParseOrDefault(coluna, RoteiroSort.Cliente),
                string.IsNullOrEmpty(direcao) || direcao.Equals("asc"));

            if (resultado.Tipo == ResultType.Valid)
            {
        
                StringBuilder csv = new StringBuilder();
                csv.AppendLine("NOME CLIENTE;TURNO;MOTORISTA;ENDEREÇO;MATERIAL;MATERIAL_COLETADO;SEGUNDA;TERÇA;QUARTA;QUINTA;SEXTA;SÁBADO;DOMINGO;OBSERVAÇÃO");

                foreach (var x in resultado.Dados)
                {
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Cliente.NomeCompleto_RazaoSocial) ? x.Cliente.NomeCompleto_RazaoSocial : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Turno.Nome_Turno) ? x.Turno.Nome_Turno : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Motorista.Nome) ? x.Motorista.Nome : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Cliente.Endereco) ? x.Cliente.Endereco : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Material.Descricao) ? x.Material.Descricao : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Material.Material_Coletado) ? x.Material.Material_Coletado : string.Empty)}\";");
                    csv.Append($"\"{(x.Segunda != null ? (Convert.ToBoolean(x.Segunda) ? "Sim" : "Não") : "Não")}\";");
                    csv.Append($"\"{(x.Terca != null ? (Convert.ToBoolean(x.Terca) ? "Sim" : "Não") : "Não")}\";");
                    csv.Append($"\"{(x.Quarta != null ? (Convert.ToBoolean(x.Quarta) ? "Sim" : "Não") : "Não")}\";");
                    csv.Append($"\"{(x.Quinta != null ? (Convert.ToBoolean(x.Quinta) ? "Sim" : "Não") : "Não")}\";");
                    csv.Append($"\"{(x.Sexta != null ? (Convert.ToBoolean(x.Sexta) ? "Sim" : "Não") : "Não")}\";");
                    csv.Append($"\"{(x.Sabado != null ? (Convert.ToBoolean(x.Sabado) ? "Sim" : "Não") : "Não")}\";");
                    csv.Append($"\"{(x.Domingo != null ? (Convert.ToBoolean(x.Domingo) ? "Sim" : "Não") : "Não")}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Observacao) ? x.Observacao : string.Empty)}\";");
                    csv.AppendLine("");
                }

                string nomeArquivo = $"roteiro{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.csv";
                byte[] bytes = Encoding.UTF8.GetBytes(csv.ToString());
                bytes = Encoding.UTF8.GetPreamble().Concat(bytes).ToArray();
                return File(bytes, "text/csv", nomeArquivo);
            }
            return Result(resultado);
        }
    }
}
