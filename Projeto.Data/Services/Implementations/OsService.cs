using Projeto.Data.Commands;
using Projeto.Data.Context;
using Projeto.Data.Entities;
using Projeto.Data.Extensions;
using Projeto.Data.Seedwork;
using Projeto.Data.ValueObjects;
using Projeto.Services.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto.Data.Services.Implementations
{
   public class OsService : IOsServices

    {
        private readonly DataColetrans dataContext;
        private readonly MailService mailService;

        public OsService(DataColetrans dataContext)
        {
            this.dataContext = dataContext;
        }

        public OsService(MailService mailService)
        {
            this.mailService = mailService;
        }

        public CommandResult Atualizar(AtualizarOsCommand command)
        {
            string entityName = "OS";
            string commandName = $"Atualizando {entityName}";
            try
            {
                command.Validate();
                if (command.Invalid)
                {
                    return CommandResult.Invalid(command.Notifications.ToNotificationsString());
                }

                var os = dataContext.OS.FirstOrDefault(x => x.Cod_OS == command.Cod_OS);

                if (os is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada("Roteiro", command.Cod_OS));
                }

                var mesRef = dataContext.MesReferencia.FirstOrDefault(x => x.Cod_MesReferencia == command.Cod_MesReferencia);

                if (mesRef is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada("Cliente", command.Cod_Cliente));
                }

                var cliente = dataContext.Cliente.FirstOrDefault(x => x.Cod_Cliente == command.Cod_Cliente);

                if (cliente is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada("Cliente", command.Cod_Cliente));
                }

                var motorista = dataContext.Motorista.FirstOrDefault(x => x.Cod_Motorista == command.Cod_Motorista);

                if (motorista is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada("Motorista", command.Cod_Motorista));
                }


                var material = dataContext.Material.FirstOrDefault(x => x.Cod_Material == command.Cod_Material);

                if (material is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada("Material", (int)command.Cod_Material));
                }

                var frota = dataContext.Frota.FirstOrDefault(x => x.Cod_Frota == command.Cod_Frota);

                if (frota is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada("Material", (int)command.Cod_Frota));
                }

                os.Atualizar(cliente,
                             mesRef,
                             material,
                             motorista,
                             frota,
                             command.Quantidade_Coletada,
                             command.Data_Coleta,
                             command.Flag_Coleta,
                             command.Flag_Envio_Email,
                             command.Flag_Cancelado,
                             DataString.FromNullableString(command.Motivo_Cancelamento),
                             command.Data_Cancelamento,
                             DataString.FromNullableString(command.Hora_Entrada),
                             DataString.FromNullableString(command.Hora_Saida));

                if (command.Flag_Envio_Email.Equals("S"))
                {
                    EnviarEmailOs(cliente.NomeCompleto_RazaoSocial, cliente.Endereco,
                                  command.Data_Coleta,command.Hora_Entrada, command.Hora_Saida,
                                  motorista.Nome, material.Descricao, command.Quantidade_Coletada, cliente.Email);
                }

                dataContext.SaveChanges();
                return CommandResult.Valid();
            }
            catch (Exception ex)
            {
                return CommandResult.Invalid(ex.Message);
            }
        }

        public CommandResult Criar(CriarOsCommand command)
        {
            string entityName = "Os";
            try
            {
                command.Validate();
                if (command.Invalid)
                {
                    return CommandResult.Invalid(command.Notifications.ToNotificationsString());
                }              
                    foreach (var cliente in command.Clientes)
                    {
                        var clienteConsulta = dataContext.Cliente.FirstOrDefault(x => x.Cod_Cliente == cliente.Cod_Cliente);

                        if (clienteConsulta is null)
                        {
                            return CommandResult.Invalid(Logs.EntidadeNaoEncontrada("Cliente", cliente.Cod_Cliente));
                        }

                        MesReferencia mesRef = dataContext.MesReferencia.FirstOrDefault(m => m.Ativo.Equals(true));

                        if (mesRef is null)
                        {
                        string message = "Para Gerar uma OS é necessário um Mês Referência Ativo.";
                        return CommandResult.Invalid(message);
                        }

                        OS os = OS.Criar(clienteConsulta,
                                            mesRef);

                        dataContext.Add(os);
                        dataContext.SaveChanges();

                        //return CommandResult.Valid();               

                    }                

                return CommandResult.Valid();

            }
            catch (Exception ex)
            {
                return CommandResult.Invalid(ex.Message);
            }
        }

        public CommandResult Remover(int cod_Os)
        {
            string entityName = "OS";
            string commandName = $"Removendo {entityName}";

            try
            {
                OS os = dataContext.OS.FirstOrDefault(x => x.Cod_OS == cod_Os);

                if (os is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada(entityName, cod_Os));
                }

                if (os.Flag_Cancelado.Equals("S") && os.Motivo_Cancelamento != null)
                {
                    string message = " A OS está Ativa, Exclusão não pode ser realizada, favor cancelar OS e informar o motivo do cancelamento.";
                    return CommandResult.Invalid(message);
                }

                dataContext.Remove(os);
                dataContext.SaveChanges();

                return CommandResult.Valid();

            }
            catch (Exception ex)
            {
                return CommandResult.Invalid(ex.Message);
            }
        }

        private void EnviarEmailOs(string NomeCompletoRazaoSocialCliente,
                                        string EnderecoCliente, DateTime? DataColeta,
                                        string horaEntrada, string horaSaida, string nomeMotorista,
                                        string material, int? quatidadeColeta, string emailCliente)
        {
            var assunto = "Guia de OS - Coletrans";


            StringBuilder corpoemail = new StringBuilder();
            corpoemail.AppendLine("<p><strong><&nbsp; Coletrans</strong></p>");
            corpoemail.AppendLine("<p style='text-align: center;'>&nbsp;</p>");
            corpoemail.AppendLine("<p style='text-align: center;'><strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Rua Ari Barroso, n&ordm;. 294 - Parque Beira Mar - Duque de Caxias - RJ.</strong></p>");
            corpoemail.AppendLine("<p style='text-align: center;'><strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Telefone : (21) 2771-4487 (21) 97576-7024</strong></p>");
            corpoemail.AppendLine("<p style='text-align: center;'>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;CNPJ : 06697682/0002-09- Inscr. Mun 780148-3 LO IN INEA02198</p>");
            corpoemail.AppendLine("<p style='text-align: center;'>&nbsp;&nbsp;</p>");
            corpoemail.AppendLine("<table style='height: 172px; margin-left: auto; margin-right: auto;' border='1' width='620' cellspacing='1'>");
            corpoemail.AppendLine("<tbody>");
            corpoemail.AppendLine("<tr>");
            corpoemail.AppendLine($"<td style='width: 297px;'>&Aacute; servi&ccedil;o de : {NomeCompletoRazaoSocialCliente} </td>");
            corpoemail.AppendLine("<td style='width: 307px;'>&nbsp;</td>");
            corpoemail.AppendLine("</tr>");
            corpoemail.AppendLine("<tr>");
            corpoemail.AppendLine($"<td style='width: 297px;'>Endere&ccedil;o : {EnderecoCliente} </td>");
            corpoemail.AppendLine("<td style='width: 307px;'>&nbsp;</td>");
            corpoemail.AppendLine("</tr>");
            corpoemail.AppendLine("<tr>");
            corpoemail.AppendLine($"<td style='width: 297px;'>Data:&nbsp; {DataColeta}</td>");
            //corpoemail.AppendLine($"<td style='width: 307px;'>Placa : {placa}</td>");
            corpoemail.AppendLine("</tr>");
            corpoemail.AppendLine($"<td style='width: 297px;'>Motorista : {nomeMotorista}</td>");
            corpoemail.AppendLine("<td style='width: 307px;'>&nbsp;</td>");
            corpoemail.AppendLine("</tr>");
            corpoemail.AppendLine("<td style='width: 297px;'>Recebido :</td>");
            corpoemail.AppendLine("<td style='width: 307px;'>&nbsp;</td>");
            corpoemail.AppendLine("</tr>");
            corpoemail.AppendLine("<tr>");
            corpoemail.AppendLine($"<td style='width: 297px;'>Material : {material}</td>");
            corpoemail.AppendLine("<td style='width: 307px;'>&nbsp;</td>");
            corpoemail.AppendLine("</tr>");
            corpoemail.AppendLine("<tr>");
            corpoemail.AppendLine($"<td style='width: 297px;'>Quanditade de Coleta : {quatidadeColeta}</td>");
            corpoemail.AppendLine("<td style='width: 307px;'>&nbsp;</td>");
            corpoemail.AppendLine("</tr>");
            corpoemail.AppendLine("</tbody>");
            corpoemail.AppendLine("/table>");

            string Body = System.IO.File.ReadAllText(@"C:\Users\thiago.leta\Desktop\Nova pasta\OS.html");

            mailService.SendMail(emailCliente, assunto, corpoemail.ToString());
          
        }

    }
}


