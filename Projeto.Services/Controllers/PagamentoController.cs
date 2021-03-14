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
using Projeto.Services.Models.Pagamento;

namespace Projeto.Services.Controllers
{
    //[Authorize("Bearer")]
    [AllowAnonymous]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ApiControllerBase
    {
        //atributo
        private readonly IPagamentoRepository pagamentoRepository;
        private readonly IMapper mapper;

        public PagamentoController(IPagamentoRepository pagamentoRepository, IMapper mapper)
        {
            this.pagamentoRepository = pagamentoRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Criar([FromServices] IPagamentoService service, [FromBody] CriarPagamentoCommand command)
        {
            return Result(service.Criar(command));
        }

        [HttpPut]
        public IActionResult Atualizar([FromServices] IPagamentoService service, [FromBody] AtualizarPagamentoCommand command)
        {
            return Result(service.Atualizar(command));
        }


        [HttpDelete("{cod_Pagamento}")]
        public IActionResult Remover(
                          [FromServices] IPagamentoService service,
                          [FromRoute] int cod_Pagamento)
        {
            return Result(service.Remover(cod_Pagamento));
        }


        [HttpGet]
        public IActionResult ObterPaginado([FromServices] IPagamentoRepository repository,

            [FromQuery] int pagina = 1,
            [FromQuery] int quantidade = 8,
            [FromQuery] string coluna = "cliente", [FromQuery] string direcao = "asc")
        {
            return Result(repository.ObterPaginado(pagina, quantidade, EnumHelpers.ParseOrDefault(coluna, PagamentoSort.NomeCliente),
                string.IsNullOrEmpty(direcao) || direcao.Equals("asc")));
        }

        [HttpGet("exportar")]
        public IActionResult Exportar([FromServices] IPagamentoRepository repository,
                                    [FromQuery] string coluna = "NomeCliente",
                                    [FromQuery] string direcao = "asc")
        {
            var resultado = repository.Obter(EnumHelpers.ParseOrDefault(coluna, PagamentoSort.NomeCliente),
                string.IsNullOrEmpty(direcao) || direcao.Equals("asc"));

            if (resultado.Tipo == ResultType.Valid)
            {

                StringBuilder csv = new StringBuilder();
                csv.AppendLine("COD_PAGAMENTO;NOME CLIENTE;MES REFERENCIA;VALOR;DATA PAGAMENTO");

                foreach (var x in resultado.Dados)
                {
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Cliente.NomeCompleto_RazaoSocial) ? x.Cliente.NomeCompleto_RazaoSocial : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Cliente.CPF_CNPJ) ? x.Cliente.CPF_CNPJ : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Cliente.Fantasia) ? x.Cliente.Fantasia : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.MesReferencia.MesAno) ? x.MesReferencia.MesAno : string.Empty)}\";");
                    csv.Append($"=\"{x.Valor}\";");                    
                    csv.Append($"=\"{x.Data}\";");
                    csv.AppendLine("");
                }

                string nomeArquivo = $"Pagamento{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.csv";
                byte[] bytes = Encoding.UTF8.GetBytes(csv.ToString());
                bytes = Encoding.UTF8.GetPreamble().Concat(bytes).ToArray();
                return File(bytes, "text/csv", nomeArquivo);
            }
            return Result(resultado);
        }
    }
}
