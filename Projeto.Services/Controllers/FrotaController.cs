using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.Data.Commands;
using Projeto.Data.Contracts;
using Projeto.Data.Extensions;
using Projeto.Data.Repository.Sorts;
using Projeto.Data.Seedwork;
using Projeto.Data.Services;
using Projeto.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Services.Controllers
{
    [Authorize("Bearer")]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class FrotaController : ApiControllerBase
    {
        [HttpPost]
        public IActionResult Criar( [FromServices] IFrotaService service,
                                    [FromBody] CriarFrotaCommand command)
        {
            return Result(service.Criar(command));
        }


        [HttpPut]
        public IActionResult Atualizar(
                [FromServices] IFrotaService service,
                [FromBody] AtualizarFrotaCommand command)
        {
            return Result(service.Atualizar(command));
        }


        [HttpDelete("{cod_Frota}")]
        public IActionResult Remover(
         [FromServices] IFrotaService service,
          [FromRoute] int cod_Frota)
        {
            return Result(service.Remover(cod_Frota));
        }

        [HttpGet]
        public IActionResult ObterPaginado(
         [FromServices] IFrotaRepository frotaRepository,
         [FromQuery] int pagina, [FromQuery] int quantidade,
         [FromQuery] string coluna = "descricao",
         [FromQuery] string direcao = "asc",
         [FromQuery] string descricao = null
         )
        {
            return Result(frotaRepository.ObterPaginado(pagina, quantidade, EnumHelpers.ParseOrDefault(coluna, FrotaSort.DescricaoFrota),
                string.IsNullOrEmpty(direcao) || direcao.Equals("asc"), DataString.FromNullableString(descricao)));
        }


        [HttpGet("exportar")]
        public IActionResult Exportar([FromServices] IFrotaRepository repository,
           [FromQuery] string coluna = "descricao",
           [FromQuery] string direcao = "asc",
           [FromQuery] string descricao = null)
        {
            var resultado = repository.Obter(EnumHelpers.ParseOrDefault(coluna, FrotaSort.DescricaoFrota),
                string.IsNullOrEmpty(direcao) || direcao.Equals("asc"), DataString.FromNullableString(descricao));

            if (resultado.Tipo == ResultType.Valid)
            {
                StringBuilder csv = new StringBuilder();
                csv.AppendLine("COD_FROTA;DESCRIÇÃO;MOTORISTA;PLACA; OBSERVAÇÃO;KM");

                foreach (var x in resultado.Dados)
                {

                    csv.Append($"\"{x.Cod_Frota}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Descricao) ? x.Descricao : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Motorista.Nome) ? x.Motorista.Nome : string.Empty)}\";");
                    csv.Append($"\"{(x.Placa != null ? x.Placa.ToString() : "")}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Observacao) ? x.Observacao : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Quilometragem) ? x.Quilometragem : string.Empty)}\";");
                    csv.AppendLine("");
                }

                string nomeArquivo = $"Frota{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.csv";
                byte[] bytes = Encoding.UTF8.GetBytes(csv.ToString());
                bytes = Encoding.UTF8.GetPreamble().Concat(bytes).ToArray();
                return File(bytes, "text/csv", nomeArquivo);
            }
            return Result(resultado);
        }

        [HttpGet("frotas")]
        public IActionResult ObterFrota([FromServices] IFrotaRepository frotaRepository)
        {
            var resultado = frotaRepository.ObterFrota();
            return Result(resultado);
        }

    }
}
