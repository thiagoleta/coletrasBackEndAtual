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
using Projeto.Services.Models.MesReferencia;
using Projeto.Data.Repository.Sorts;
using System.Globalization;

namespace Projeto.Services.Controllers
{
    //[Authorize("Bearer")]
    [AllowAnonymous]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class MesReferenciaController : ApiControllerBase
    {
        public const string FORMATO_DATA_PADRAO = "dd/MM/yyyy";
        //atributo
        private readonly IMesReferenciaRepository mesreferenciaRepository;
        private readonly IMapper mapper;

        public MesReferenciaController(IMesReferenciaRepository mesreferenciaRepository, IMapper mapper)
        {
            this.mesreferenciaRepository = mesreferenciaRepository;
            this.mapper = mapper;
        }


        [HttpPost]
        public IActionResult Criar(
         [FromServices] IMesReferenciaService service,
         [FromBody] CriarMesReferenciaCommand command)
        {
            return Result(service.Criar(command));
        }



        [HttpPut]
        public IActionResult Atualizar(
         [FromServices] IMesReferenciaService service,
         [FromBody] AtualizarMesReferenciaCommand command)
        {
            return Result(service.Atualizar(command));
        }


        [HttpDelete("{cod_MesReferencia}")]
        public IActionResult Remover(
     [FromServices] IMesReferenciaService service,
      [FromRoute] int cod_MesReferencia)
        {
            return Result(service.Remover(cod_MesReferencia));
        }



        [HttpGet]
        public IActionResult ObterPaginado(
         [FromServices] IMesReferenciaRepository mesReferanciaRepository,
         [FromQuery] int pagina, [FromQuery] int quantidade,
         [FromQuery] string coluna = "mesAno",
         [FromQuery] string direcao = "asc",
         [FromQuery] string mesAno = null
         )
        {

            var resultado = mesReferanciaRepository.ObterPaginado(pagina, quantidade, EnumHelpers.ParseOrDefault(coluna, MesReferenciaSort.MesAno),
                string.IsNullOrEmpty(direcao) || direcao.Equals("asc"), DataString.FromNullableString(mesAno));

            if (resultado.Tipo == ResultType.Valid)
            {


                foreach (var mesRef in resultado.Dados.Data)
                {
                  
                    if (mesRef.DataInicio != null)
                    {
                        string data = mesRef.DataInicio.ToShortDateString();

                        //ToShortDateString()

                        //mesRef.DataInicio = data;
                    }                     

                }

                

            }
            return Result(resultado);
        }
  



        [HttpGet("exportar")]
        public IActionResult Exportar([FromServices] IMesReferenciaRepository repository,
         [FromQuery] string coluna = "nome", [FromQuery] string direcao = "asc", [FromQuery] string nome = null)
        {
            var resultado = repository.Obter(EnumHelpers.ParseOrDefault(coluna, MesReferenciaSort.MesAno),
                string.IsNullOrEmpty(direcao) || direcao.Equals("asc"), DataString.FromNullableString(nome));

            if (resultado.Tipo == ResultType.Valid)
            {
                StringBuilder csv = new StringBuilder();
                csv.AppendLine("COD_MES_REFERÊNCIA; MES/ANO; DATA INICIO REFERÊNCIA; DATA FIM REFERÊNCIA;");

                foreach (var x in resultado.Dados)
                {

                    csv.Append($"\"{(!string.IsNullOrEmpty(x.MesAno) ? x.MesAno : string.Empty)}\";");
                    csv.Append($"\"{(x.DataInicio != null ? x.DataInicio.ToShortDateString() : string.Empty)}\";");
                    csv.Append($"\"{(x.DataTermino != null ? x.DataTermino.Value.ToShortDateString() : string.Empty)}\";");
                    csv.AppendLine("");
                }

                string nomeArquivo = $"MesReferencia{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.csv";
                byte[] bytes = Encoding.UTF8.GetBytes(csv.ToString());
                bytes = Encoding.UTF8.GetPreamble().Concat(bytes).ToArray();
                return File(bytes, "text/csv", nomeArquivo);
            }
            return Result(resultado);
        }

        [HttpGet("mesativos")]
        public IActionResult ObterClientesAtivos([FromServices] IMesReferenciaRepository repository)
        {
            var resultado = repository.ObterMesRefAtivos();
            return Result(resultado);
        }
    }
}
