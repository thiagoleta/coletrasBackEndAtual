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
using Projeto.Services.Models.Usuario;
using Projeto.Services.Util;

namespace Projeto.Services.Controllers
{
    //[Authorize("Bearer")]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioRefatorado : ApiControllerBase
    {
        [HttpPost]
        public IActionResult Criar([FromServices] IUsuarioService service, [FromBody] CriarUsuarioCommand command)
        {
            return Result(service.Criar(command));
        }

        [HttpPut]
        public IActionResult Atualizar([FromServices] IUsuarioService service, [FromBody] AtualizarUsuarioCommand command)
        {
            return Result(service.Atualizar(command));
        }

        [HttpDelete("{cod_Usuario}")]
        public IActionResult Remover(
                              [FromServices] IUsuarioService service,
                              [FromRoute] int cod_Usuario)
        {
            return Result(service.Remover(cod_Usuario));
        }


        [HttpGet]
        public IActionResult ObterPaginado([FromServices] IUsuarioRepository repository,

             [FromQuery] int pagina = 1,
             [FromQuery] int quantidade = 8,
             [FromQuery] string coluna = "nome", [FromQuery] string direcao = "asc")
        {
            return Result(repository.ObterPaginado(pagina, quantidade, EnumHelpers.ParseOrDefault(coluna, UsuarioSort.Nome),
                string.IsNullOrEmpty(direcao) || direcao.Equals("asc")));
        }

        [HttpGet("exportar")]
        public IActionResult Exportar([FromServices] IUsuarioRepository repository,
                                 [FromQuery] string coluna = "nome",
                                 [FromQuery] string direcao = "asc")
        {
            var resultado = repository.ObterUsuarios(EnumHelpers.ParseOrDefault(coluna, UsuarioSort.Nome),
                string.IsNullOrEmpty(direcao) || direcao.Equals("asc"));

            if (resultado.Tipo == ResultType.Valid)
            {

                StringBuilder csv = new StringBuilder();
                csv.AppendLine("COD_USUARIO;NOME USUARIO;EMAIL;PERFIL_NOME;DATA CRIAÇÃO");

                foreach (var x in resultado.Dados)
                {
                    csv.Append($"\"{x.Cod_Usuario}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Nome) ? x.Nome : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Email) ? x.Email : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Perfil.Nome_Perfil) ? x.Perfil.Nome_Perfil : string.Empty)}\";");
                    csv.Append($"=\"{x.DataCriacao}\";");
                    csv.AppendLine("");
                }

                string nomeArquivo = $"Usuario{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.csv";
                byte[] bytes = Encoding.UTF8.GetBytes(csv.ToString());
                bytes = Encoding.UTF8.GetPreamble().Concat(bytes).ToArray();
                return File(bytes, "text/csv", nomeArquivo);
            }
            return Result(resultado);
        }

        [HttpGet("usuarios")]
        public IActionResult ObterUsuarios([FromServices] IUsuarioRepository repository)
        {
            var resultado = repository.ObterUsuariosSelect();
            return Result(resultado);
        }
    }
}
