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
using Projeto.Services.Models.Cliente;

namespace Projeto.Services.Controllers
{
    //[Authorize("Bearer")]
    [AllowAnonymous]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]

    public class ClienteController : ApiControllerBase
    {

        [HttpPost]
        public IActionResult Criar(
             [FromServices] IClienteService service,
             [FromBody] CriarClienteCommand command)
        {
            return Result(service.Criar(command));
        }


        [HttpPut]
        public IActionResult Atualizar(
                [FromServices] IClienteService service,
                [FromBody] AtualizarClienteCommand command)
        {
            return Result(service.Atualizar(command));
        }



        [HttpDelete("{cod_Cliente}")]
        public IActionResult Remover(
             [FromServices] IClienteService service,
              [FromRoute] int cod_Cliente)
        {
            return Result(service.Remover(cod_Cliente));
        }

        [HttpGet]
        public IActionResult ObterPaginado(
           [FromServices] IClienteRepository clienteRepository,
           [FromQuery] int pagina = 1,
           [FromQuery] int quantidade = 8,
           [FromQuery] string coluna = "nomeCompleto_RazaoSocial",
           [FromQuery] string direcao = "asc",
           [FromQuery] string nomeCompleto_RazaoSocial = null,
           [FromQuery] string cpF_CNPJ = null)
        {
            var resultado = clienteRepository.ObterPaginado(
                EnumHelpers.ParseOrDefault(coluna, ClienteSort.NomeCompleto_RazaoSocial), string.IsNullOrEmpty(direcao) || direcao.Equals("asc"),
                pagina, quantidade,DataString.FromNullableString(nomeCompleto_RazaoSocial), DataString.FromNullableString(cpF_CNPJ));

            return Result(resultado);
        }



        [HttpGet("exportar")]
        public IActionResult Exportar([FromServices] IClienteRepository clienteRepository,
           [FromQuery] string coluna = "nomeCompleto_RazaoSocial",
           [FromQuery] string direcao = "asc",
           [FromQuery] string nomeCompleto_RazaoSocial = null,
           [FromQuery] string cpF_CNPJ = null)
        {
            var resultado = clienteRepository.Obter(
                 EnumHelpers.ParseOrDefault(coluna, ClienteSort.NomeCompleto_RazaoSocial), string.IsNullOrEmpty(direcao) || direcao.Equals("asc"),
                 DataString.FromNullableString(nomeCompleto_RazaoSocial), DataString.FromNullableString(cpF_CNPJ));

            if (resultado.Tipo == ResultType.Valid)
            {
                StringBuilder csv = new StringBuilder();
                csv.AppendLine("COD_CLIENTE;NOME COMPLETO; FANTASIA; INSCRIÇÃO ESTADUAL; LOGRADOURO; ENDEREÇO; BAIRRO; COMPLEMENTO; CIDADE; CEP; UF; TELEFONES; FUNÇÃO; EMAIL; ATIVO; OBSERVAÇÃO; REFERENCIA");

                foreach (var x in resultado.Dados)
                {

                    csv.Append($"\"{x.Cod_Cliente}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.NomeCompleto_RazaoSocial) ? x.NomeCompleto_RazaoSocial : string.Empty)}\";");                    
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Fantasia) ? x.Fantasia : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Insc_Estadual) ? x.Insc_Estadual : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Logradouro) ? x.Logradouro : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Endereco) ? x.Endereco : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Bairro) ? x.Bairro : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Complemento) ? x.Complemento : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Cidade) ? x.Cidade : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.CEP) ? x.CEP : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.UF) ? x.UF : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Telefones) ? x.Telefones : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Funcao) ? x.Funcao : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Email) ? x.Email : string.Empty)}\";");
                    csv.Append($"\"{(x.Flag_Ativo != null ? (Convert.ToBoolean(x.Flag_Ativo) ? "Sim" : "Não") : "Não")}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Observacao) ? x.Observacao : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Referencia) ? x.Referencia : string.Empty)}\";");
                    csv.AppendLine("");
                }
                
     
                string nomeArquivo = $"Cliente{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.csv";
                byte[] bytes = Encoding.UTF8.GetBytes(csv.ToString());
                bytes = Encoding.UTF8.GetPreamble().Concat(bytes).ToArray();
                return File(bytes, "text/csv", nomeArquivo);
            }
            return Result(resultado);
        }

        [HttpGet("clientesativos")]
        public IActionResult ObterClientesAtivos([FromServices] IClienteRepository clienteRepository)
        {
            var resultado = clienteRepository.ObterClientesAtivos();
            return Result(resultado);
        }

    }
}
