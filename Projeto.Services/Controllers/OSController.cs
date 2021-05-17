using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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


namespace Projeto.Services.Controllers
{
    //[Authorize("Bearer")]
    [AllowAnonymous]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class OSController : ApiControllerBase
    {

        [HttpPost]
        public IActionResult Criar(
         [FromServices] IOsServices service,
         [FromBody] CriarOsCommand command)
        {
            return Result(service.Criar(command));
        }

        [HttpPut]
        public IActionResult Atualizar(
                [FromServices] IOsServices service,
                [FromBody] AtualizarOsCommand command)
        {
            return Result(service.Atualizar(command));
        }

        [HttpDelete("{cod_Os}")]
        public IActionResult Remover(
             [FromServices] IOsServices service,
              [FromRoute] int cod_Os)
        {
            return Result(service.Remover(cod_Os));
        }

        [HttpGet]
        public IActionResult ObterPaginado(
          [FromServices] IOSRepository osRepository,
          [FromQuery] int pagina = 1,
          [FromQuery] int quantidade = 8,
          [FromQuery] string coluna = "nomeCompleto_RazaoSocial",
          [FromQuery] string direcao = "asc",
          [FromQuery] string nomeCompleto_RazaoSocial = null,
          [FromQuery] string mesAno = null)
        {
            var resultado = osRepository.ObterPaginado(
                EnumHelpers.ParseOrDefault(coluna, OsSort.NomeCompleto_RazaoSocial), string.IsNullOrEmpty(direcao) || direcao.Equals("asc"),
                pagina, quantidade, DataString.FromNullableString(nomeCompleto_RazaoSocial), DataString.FromNullableString(mesAno));

            return Result(resultado);
        }

        [HttpGet("exportar")]
        public IActionResult Exportar([FromServices] IOSRepository osRepository,
           [FromQuery] string coluna = "nomeCompleto_RazaoSocial",
           [FromQuery] string direcao = "asc",
           [FromQuery] string nomeCompleto_RazaoSocial = null,
           [FromQuery] string mesAno = null)
        {
            var resultado = osRepository.Obter(
                 EnumHelpers.ParseOrDefault(coluna, OsSort.NomeCompleto_RazaoSocial), string.IsNullOrEmpty(direcao) || direcao.Equals("asc"),
                 DataString.FromNullableString(nomeCompleto_RazaoSocial), DataString.FromNullableString(mesAno));

            if (resultado.Tipo == ResultType.Valid)
            {
                StringBuilder csv = new StringBuilder();
                csv.AppendLine("COD_OS;NOME CLIENTE;FANTASIA;INSCRIÇÃO ESTADUAL;ENDEREÇO;TELEFONES;EMAIL;MÊS/ANO;DATA DA GERAÇÃO OS;NOME MOTORISTA;QUANTIDADE COLETADA;DATA COLETA;MATERIAL/HORA ENTRADA;HORA SAIDA;PLACA;CANCELADA;MOTIVO CANCELAMENTO;COLETADA;ENVIAR EMAIL;");

                foreach (var x in resultado.Dados)
                {
                    csv.Append($"\"{x.Cod_Cliente}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Cliente.NomeCompleto_RazaoSocial) ? x.Cliente.NomeCompleto_RazaoSocial : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Cliente.Fantasia) ? x.Cliente.Fantasia : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Cliente.Insc_Estadual) ? x.Cliente.Insc_Estadual : string.Empty)}\";");                    
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Cliente.Endereco) ? x.Cliente.Endereco : string.Empty)}\";");                    
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Cliente.Telefones) ? x.Cliente.Telefones : string.Empty)}\";");                    
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Cliente.Email) ? x.Cliente.Email : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.MesReferencia.MesAno) ? x.MesReferencia.MesAno : string.Empty)}\";");
                    csv.Append($"=\"{x.Data_Geracao}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Motorista.Nome) ? x.Motorista.Nome : string.Empty)}\";");
                    csv.Append($"=\"{x.Quantidade_Coletada}\";");
                    csv.Append($"=\"{x.Data_Coleta}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Material.Descricao) ? x.Material.Descricao : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Hora_Entrada) ? x.Hora_Entrada : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Hora_Saida) ? x.Hora_Saida : string.Empty)}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Placa) ? x.Placa : string.Empty)}\";");
                    csv.Append($"\"{(x.Flag_Cancelado != null ? (Convert.ToBoolean(x.Flag_Cancelado) ? "Sim" : "Não") : "Não")}\";");
                    csv.Append($"\"{(!string.IsNullOrEmpty(x.Motivo_Cancelamento) ? x.Motivo_Cancelamento : string.Empty)}\";");
                    csv.Append($"\"{(x.Flag_Coleta != null ? (Convert.ToBoolean(x.Flag_Coleta) ? "Sim" : "Não") : "Não")}\";");
                    csv.Append($"\"{(x.Flag_Envio_Email != null ? (Convert.ToBoolean(x.Flag_Envio_Email) ? "Sim" : "Não") : "Não")}\";");                    
                    csv.AppendLine("");
                }


                string nomeArquivo = $"OS{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.csv";
                byte[] bytes = Encoding.UTF8.GetBytes(csv.ToString());
                bytes = Encoding.UTF8.GetPreamble().Concat(bytes).ToArray();
                return File(bytes, "text/csv", nomeArquivo);
            }
            return Result(resultado);
        }


        //private void EnviarEmailOs(string NomeCompletoRazaoSocialCliente,
        //                                        string EnderecoCliente, DateTime DataColeta,
        //                                        string horaEntrada, string horaSaida, string nomeMotorista,
        //                                        string recebido, string material, int quatidadeColeta, string placa, string emailCliente)
        //{
        //    var assunto = "Guia de OS - Coletrans";


        //    StringBuilder corpoemail = new StringBuilder();
        //    corpoemail.AppendLine("<p><strong><&nbsp; Coletrans</strong></p>");
        //    corpoemail.AppendLine("<p style='text-align: center;'>&nbsp;</p>");
        //    corpoemail.AppendLine("<p style='text-align: center;'><strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Rua Ari Barroso, n&ordm;. 294 - Parque Beira Mar - Duque de Caxias - RJ.</strong></p>");
        //    corpoemail.AppendLine("<p style='text-align: center;'><strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Telefone : (21) 2771-4487 (21) 97576-7024</strong></p>");
        //    corpoemail.AppendLine("<p style='text-align: center;'>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;CNPJ : 06697682/0002-09- Inscr. Mun 780148-3 LO IN INEA02198</p>");
        //    corpoemail.AppendLine("<p style='text-align: center;'>&nbsp;&nbsp;</p>");
        //    corpoemail.AppendLine("<table style='height: 172px; margin-left: auto; margin-right: auto;' border='1' width='620' cellspacing='1'>");
        //    corpoemail.AppendLine("<tbody>");
        //    corpoemail.AppendLine("<tr>");
        //    corpoemail.AppendLine($"<td style='width: 297px;'>&Aacute; servi&ccedil;o de : {NomeCompletoRazaoSocialCliente} </td>");
        //    corpoemail.AppendLine("<td style='width: 307px;'>&nbsp;</td>");
        //    corpoemail.AppendLine("</tr>");
        //    corpoemail.AppendLine("<tr>");
        //    corpoemail.AppendLine($"<td style='width: 297px;'>Endere&ccedil;o : {EnderecoCliente} </td>");
        //    corpoemail.AppendLine("<td style='width: 307px;'>&nbsp;</td>");
        //    corpoemail.AppendLine("</tr>");
        //    corpoemail.AppendLine("<tr>");
        //    corpoemail.AppendLine($"<td style='width: 297px;'>Data:&nbsp; {DataColeta}</td>");
        //    corpoemail.AppendLine($"<td style='width: 307px;'>Placa : {placa}</td>");
        //    corpoemail.AppendLine("</tr>");
        //    corpoemail.AppendLine($"<td style='width: 297px;'>Motorista : {nomeMotorista}</td>");
        //    corpoemail.AppendLine("<td style='width: 307px;'>&nbsp;</td>");
        //    corpoemail.AppendLine("</tr>");
        //    corpoemail.AppendLine("<td style='width: 297px;'>Recebido :</td>");
        //    corpoemail.AppendLine("<td style='width: 307px;'>&nbsp;</td>");
        //    corpoemail.AppendLine("</tr>");
        //    corpoemail.AppendLine("<tr>");
        //    corpoemail.AppendLine($"<td style='width: 297px;'>Material : {material}</td>");
        //    corpoemail.AppendLine("<td style='width: 307px;'>&nbsp;</td>");
        //    corpoemail.AppendLine("</tr>");
        //    corpoemail.AppendLine("<tr>");
        //    corpoemail.AppendLine($"<td style='width: 297px;'>Quanditade de Coleta : {quatidadeColeta}</td>");
        //    corpoemail.AppendLine("<td style='width: 307px;'>&nbsp;</td>");
        //    corpoemail.AppendLine("</tr>");
        //    corpoemail.AppendLine("</tbody>");
        //    corpoemail.AppendLine("/table>");

        //    string Body = System.IO.File.ReadAllText(@"C:\Users\thiago.leta\Desktop\Nova pasta\OS.html");

        //    mailService.SendMail(emailCliente, assunto, corpoemail.ToString());
        //}

    }
}
