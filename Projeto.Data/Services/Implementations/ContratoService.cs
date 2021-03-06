﻿using Projeto.Data.Commands;
using Projeto.Data.Context;
using Projeto.Data.Entities;
using Projeto.Data.Extensions;
using Projeto.Data.Seedwork;
using Projeto.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto.Data.Services.Implementations
{
   public class ContratoService : IContratoService
    {
        private readonly DataColetrans dataContext;

        public ContratoService(DataColetrans dataContext)
        {
            this.dataContext = dataContext;
        }

        public CommandResult Atualizar(AtualizarContratoCommand command)
        {
            string entityName = "Contrato";
            try
            {
                command.Validate();
                if (command.Invalid)
                {
                    return CommandResult.Invalid(command.Notifications.ToNotificationsString());
                }

                Contrato contrato = dataContext.Contrato.FirstOrDefault(x => x.Cod_Contrato == command.Cod_Contrato);

                if (contrato is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada(entityName, command.Cod_Contrato));
                }

                var cliente = dataContext.Cliente.FirstOrDefault(x => x.Cod_Cliente == command.Cod_Cliente);

                if (cliente is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada("Cliente", command.Cod_Cliente));
                }

                contrato.Atualizar(
                   command.ColetaContratada,
                   command.ValorLimite,
                   command.ValorUnidade,                   
                   DataString.FromNullableString(command.MotivoCancelamento),
                   command.DataCancelamento,
                   DataString.FromString(command.FlagTermino),
                   command.DataInicio,
                   command.DataTermino,
                   cliente);
                
                dataContext.SaveChanges();
                return CommandResult.Valid();
            }
            catch (Exception ex)
            {
                return CommandResult.Invalid(ex.Message);
            }

        }

        public CommandResult Criar(CriarContratoCommand command)
        {
            string entityName = "Contrato";
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



                    Contrato contrato = Contrato.Criar(command.ColetaContratada,
                   command.ValorLimite,
                   command.ValorUnidade,
                   DataString.FromNullableString(command.MotivoCancelamento),
                   command.DataCancelamento,
                   command.FlagTermino,
                   command.DataInicio,
                   command.DataTermino,
                   cliente);


                    dataContext.Add(contrato);
                    dataContext.SaveChanges();


                    return CommandResult.Valid();
                }
                catch (Exception ex)
                {

                    return CommandResult.Invalid(ex.Message);
                }
            }
        }

        public CommandResult Remover(int cod_Contrato)
        {
            string entityName = "Contrato";
            string commandName = $"Removendo {entityName}";

            try
            {
                Contrato contrato = dataContext.Contrato.FirstOrDefault(x => x.Cod_Contrato == cod_Contrato);

                if (contrato is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada(entityName, cod_Contrato));
                }

                if (contrato.FlagTermino.Equals("S"))
                {
                    string message = "Existe um contrato Ativo, Exclusão não pode ser realizada. ";
                    return CommandResult.Invalid(message);
                }

                dataContext.Remove(contrato);
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
