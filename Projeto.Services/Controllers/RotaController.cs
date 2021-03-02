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
using Projeto.Data.ValueObjects;
using Projeto.Services.Models.Rota;

namespace Projeto.Services.Controllers
{
    //[Authorize("Bearer")]
    [AllowAnonymous]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class RotaController : ApiControllerBase
    {
        //atributo
        private readonly IRotaRepository rotaRepository;        
        private readonly IMapper mapper;

        public RotaController(IRotaRepository rotaRepository, IMapper mapper)
        {
            this.rotaRepository = rotaRepository;
            this.mapper = mapper;
        }

        
        [HttpGet]
        public IActionResult ObterPaginado(
      [FromServices] IRotaRepository rotaRepository,
      [FromQuery] int pagina, [FromQuery] int quantidade,
      [FromQuery] string coluna = "nome",
      [FromQuery] string direcao = "asc",
      [FromQuery] string nome = null
      )
        {
            return Result(rotaRepository.ObterPaginado(pagina, quantidade, EnumHelpers.ParseOrDefault(coluna, RotaSort.Nome),
                string.IsNullOrEmpty(direcao) || direcao.Equals("asc"), DataString.FromNullableString(nome)));
        }


        [HttpGet("exportar")]
        public IActionResult Exportar([FromServices] IRotaRepository repository,
                                     [FromQuery] string coluna = "nome",
                                     [FromQuery] string direcao = "asc",
                                     [FromQuery] string nome = null)
        {
            var resultado = repository.Obter(EnumHelpers.ParseOrDefault(coluna, RotaSort.Nome),
                string.IsNullOrEmpty(direcao) || direcao.Equals("asc"), DataString.FromNullableString(nome));

            if (resultado.Tipo == ResultType.Valid)
            {
                StringBuilder csv = new StringBuilder();
                csv.AppendLine("COD_ROTA; NOME; COMPOSIÇÃO DA ROTA; ATIVO; OBSERVAÇÃO");

                foreach (var x in resultado.Dados)
                {
                    csv.Append($"\"{x.Cod_Rota}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Nome) ? x.Nome : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Composicao_Rota) ? x.Composicao_Rota : string.Empty)}\";");
                    csv.Append($"\"{(x.Flag_Ativo != null ? (Convert.ToBoolean(x.Flag_Ativo) ? "Sim" : "Não") : "Não")}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Observacao) ? x.Observacao : string.Empty)}\";");                    
                    csv.AppendLine("");
                }

                string nomeArquivo = $"Rota{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.csv";
                byte[] bytes = Encoding.UTF8.GetBytes(csv.ToString());
                bytes = Encoding.UTF8.GetPreamble().Concat(bytes).ToArray();
                return File(bytes, "text/csv", nomeArquivo);
            }
            return Result(resultado);
        }


        [HttpDelete("{cod_Rota}")]
        public IActionResult Remover(
                            [FromServices] IRotaService service,
                            [FromRoute] int cod_Rota)
        {
            return Result(service.Remover(cod_Rota));
        }

        [HttpPut]
        public IActionResult Atualizar(
                                [FromServices] IRotaService service,
                                [FromBody] AtualizarRotaCommand command)
        {
            return Result(service.Atualizar(command));
        }

        [HttpPost]
        public IActionResult Criar(
                              [FromServices] IRotaService service,
                              [FromBody] CriarRotaCommand command)
        {
            return Result(service.Criar(command));
        }

        [HttpGet("rotasativas")]
        public IActionResult ObterRotasAtivas([FromServices] IRotaRepository rotaRepository)
        {
            var resultado = rotaRepository.ObterRotasAtivas();
            return Result(resultado);
        }


    }
}
