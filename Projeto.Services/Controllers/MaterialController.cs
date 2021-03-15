using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using Projeto.Services.Models.Material;
using Projeto.Data.Extensions;
using Projeto.Data.Seedwork;
using Projeto.Data.ValueObjects;
using Projeto.Data.Repository.Sorts;
using System.Text;
using System.Linq;
using Projeto.Data.Services;
using Projeto.Data.Commands;

namespace Projeto.Services.Controllers
{

    [AllowAnonymous]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ApiControllerBase
    {
 
        [HttpPost]
        public IActionResult Criar(
                [FromServices] IMaterialService service,
                [FromBody] CriarMaterialCommand command)
        {
            return Result(service.Criar(command));
        }


        [HttpPut]
        public IActionResult Atualizar(
                [FromServices] IMaterialService service,
                [FromBody] AtualizarMaterialCommand command)
        {
            return Result(service.Atualizar(command));
        }

        

        [HttpDelete("{cod_Material}")]
        public IActionResult Remover(
             [FromServices] IMaterialService service,
              [FromRoute] int cod_Material)
        {
            return Result(service.Remover(cod_Material));
        }


        [HttpGet]
        public IActionResult ObterPaginado(
             [FromServices] IMaterialRepository materialRepository,
             [FromQuery] int pagina, [FromQuery] int quantidade,
             [FromQuery] string coluna = "descricao",
             [FromQuery] string direcao = "asc",
             [FromQuery] string descricao = null
             )
        {
            return Result(materialRepository.ObterPaginado(pagina, quantidade, EnumHelpers.ParseOrDefault(coluna, MaterialSort.Descricao),
                string.IsNullOrEmpty(direcao) || direcao.Equals("asc"), DataString.FromNullableString(descricao)));
        }

        [HttpGet("exportar")]
        public IActionResult Exportar([FromServices] IMaterialRepository repository,
           [FromQuery] string coluna = "descricao", 
           [FromQuery] string direcao = "asc", 
           [FromQuery] string descricao = null)
        {
            var resultado = repository.Obter(EnumHelpers.ParseOrDefault(coluna, MaterialSort.Descricao),
                string.IsNullOrEmpty(direcao) || direcao.Equals("asc"), DataString.FromNullableString(descricao));

            if (resultado.Tipo == ResultType.Valid)
            {
                StringBuilder csv = new StringBuilder();
                csv.AppendLine("COD_MATERIAL;DESCRIÇÃO; VOLUME; OBSERVAÇÃO; MATERIAL COLETADO");

                foreach (var x in resultado.Dados)
                {

                    csv.Append($"\"{x.Cod_Material}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Descricao) ? x.Descricao : string.Empty)}\";");
                    csv.Append($"\"{(x.Volume != null ? x.Volume.ToString() : "")}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Observacao) ? x.Observacao : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Material_Coletado) ? x.Material_Coletado : string.Empty)}\";");
                    csv.AppendLine("");
                }

                string nomeArquivo = $"Material{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.csv";
                byte[] bytes = Encoding.UTF8.GetBytes(csv.ToString());
                bytes = Encoding.UTF8.GetPreamble().Concat(bytes).ToArray();
                return File(bytes, "text/csv", nomeArquivo);
            }
            return Result(resultado);
        }

        [HttpGet("materiais")]
        public IActionResult ObterMateriais([FromServices] IMaterialRepository materialRepository)
        {
            var resultado = materialRepository.ObterMateriais();
            return Result(resultado);
        }
    }
}
