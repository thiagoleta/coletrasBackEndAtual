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
using Projeto.Data.ValueObjects;
using Projeto.Services.Models.Perfil;

namespace Projeto.Services.Controllers
{

    [AllowAnonymous]
    //[Authorize("Bearer")]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ApiControllerBase
    {

        [HttpPost]
        public IActionResult Criar(
           [FromServices] IPerfilService service,
           [FromBody] CriarPerfilCommand command)
        {
            return Result(service.Criar(command));
        }


        [HttpPut]
        public IActionResult Atualizar([FromServices] IPerfilService service, [FromBody] AtualizarPerfilCommand command)
        {
            return Result(service.Atualizar(command));
        }


        [HttpDelete("{cod_Perfil}")]
        public IActionResult Remover(
                              [FromServices] IPerfilService service,
                              [FromRoute] int cod_Perfil)
        {
            return Result(service.Remover(cod_Perfil));
        }

        [HttpGet]
        public IActionResult ObterPaginado([FromServices] IPerfilRepository repository,

                 [FromQuery] int pagina = 1,
                 [FromQuery] int quantidade = 8,
                 [FromQuery] string coluna = "nomePerfil", [FromQuery] string direcao = "asc")
        {
            return Result(repository.ObterPaginado(pagina, quantidade, EnumHelpers.ParseOrDefault(coluna, PerfilSort.NomePerfil),
                string.IsNullOrEmpty(direcao) || direcao.Equals("asc")));
        }


        [HttpGet("exportar")]
        public IActionResult Exportar([FromServices] IPerfilRepository repository,
                                     [FromQuery] string coluna = "nomePerfil",
                                     [FromQuery] string direcao = "asc",
                                     [FromQuery] string nomePerfil = null)
        {
            var resultado = repository.Obter(EnumHelpers.ParseOrDefault(coluna, PerfilSort.NomePerfil),
             string.IsNullOrEmpty(direcao) || direcao.Equals("asc"));

            if (resultado.Tipo == ResultType.Valid)
            {
                StringBuilder csv = new StringBuilder();
                csv.AppendLine("COD_ROTA; NOME; COMPOSIÇÃO DA ROTA; ATIVO; OBSERVAÇÃO");

                foreach (var x in resultado.Dados)
                {
                    csv.Append($"\"{x.Cod_Perfil}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Nome_Perfil) ? x.Nome_Perfil : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Usuario.Nome) ? x.Usuario.Nome : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Usuario.Email) ? x.Usuario.Email : string.Empty)}\";");                    
                    csv.AppendLine("");
                }

                string nomeArquivo = $"Perfil{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.csv";
                byte[] bytes = Encoding.UTF8.GetBytes(csv.ToString());
                bytes = Encoding.UTF8.GetPreamble().Concat(bytes).ToArray();
                return File(bytes, "text/csv", nomeArquivo);
            }
            return Result(resultado);
        }


    }
}
