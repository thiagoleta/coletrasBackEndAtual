using Projeto.Data.Commands;
using Projeto.Data.Context;
using Projeto.Data.Entities;
using Projeto.Data.Extensions;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto.Data.Services.Implementations
{
    public class PagamentoService : IPagamentoService
    {
        private readonly DataColetrans dataContext;

        public PagamentoService(DataColetrans dataContext)
        {
            this.dataContext = dataContext;
        }

        public CommandResult Atualizar(AtualizarPagamentoCommand command)
        {
            string entityName = "Pagamento";
            try
            {
                command.Validate();
                if (command.Invalid)
                {
                    return CommandResult.Invalid(command.Notifications.ToNotificationsString());
                }

                Pagamento pagamento = dataContext.Pagamento.FirstOrDefault(x => x.Cod_Pagamento == command.Cod_Pagamento);

                if (pagamento is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada(entityName, command.Cod_Pagamento));
                }


                var cliente = dataContext.Cliente.FirstOrDefault(x => x.Cod_Cliente == command.Cod_Cliente);

                if (cliente is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada("Cliente", command.Cod_Cliente));
                }

                var mesReferencia = dataContext.MesReferencia.FirstOrDefault(x => x.Cod_MesReferencia == command.Cod_MesReferencia);

                if (mesReferencia is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada("MesReferencia", command.Cod_MesReferencia));
                }

                pagamento.Atualizar(
                   command.Valor,
                   command.Data,
                   mesReferencia,
                   cliente);

                return CommandResult.Valid();

            }
            catch (Exception ex)
            {
                return CommandResult.Invalid(ex.Message);
            }

        }

        public CommandResult Criar(CriarPagamentoCommand command)
        {
            string entityName = "Pagamento";
            {
                try
                {
                    command.Validate();
                    if (command.Invalid)
                    {
                        return CommandResult.Invalid(command.Notifications.ToNotificationsString());
                    }

                    var cliente = dataContext.Cliente.FirstOrDefault(x => x.Cod_Cliente == command.Cod_Cliente);

                    if (cliente is null)
                    {
                        return CommandResult.Invalid(Logs.EntidadeNaoEncontrada("Cliente", command.Cod_Cliente));
                    }

                    var mesReferencia = dataContext.MesReferencia.FirstOrDefault(x => x.Cod_MesReferencia == command.Cod_MesReferencia);

                    if (mesReferencia is null)
                    {
                        return CommandResult.Invalid(Logs.EntidadeNaoEncontrada("MesReferencia", command.Cod_MesReferencia));
                    }

                    Pagamento pagamento = Pagamento.Criar(command.Valor,
                   command.Data,
                   mesReferencia,
                   cliente);

                    dataContext.Add(pagamento);
                    dataContext.SaveChanges();
                    return CommandResult.Valid();
                }
                catch (Exception ex)
                {

                    return CommandResult.Invalid(ex.Message);
                }
            }
        }

        public CommandResult Remover(int cod_Pagamento)
        {
            string entityName = "Pagamento";
            string commandName = $"Removendo {entityName}";
            try
            {
                Pagamento pagamento = dataContext.Pagamento.FirstOrDefault(x => x.Cod_Pagamento == cod_Pagamento);

                if (pagamento is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada(entityName, cod_Pagamento));
                }

                dataContext.Remove(pagamento);
                dataContext.SaveChanges();
                return CommandResult.Valid();
            }
            catch (Exception ex)
            {
                return CommandResult.Invalid(ex.Message);
            }
        }
    }

    }

