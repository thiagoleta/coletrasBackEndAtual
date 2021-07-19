using Projeto.Data.Commands;
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
    public class MotoristaService : IMotoristaService
    {
        private readonly DataColetrans dataContext;

        public MotoristaService(DataColetrans dataContext)
        {
            this.dataContext = dataContext;
        }

        public CommandResult Atualizar(AtualizarMotoristaCommand command)
        {

            string entityName = "Motorista";
            string commandName = $"Atualizando {entityName}";
            try
            {
                command.Validate();
                if (command.Invalid)
                {
                    return CommandResult.Invalid(command.Notifications.ToNotificationsString());
                }

                Motorista motorista = dataContext.Motorista.FirstOrDefault(x => x.Cod_Motorista == command.Cod_Motorista);

                if (motorista is null)
                {

                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada(entityName, command.Cod_Motorista));
                }

                if (dataContext.Motorista.Any(x => x.Nome == command.Nome.ToUpper() && x.Cod_Motorista != command.Cod_Motorista))
                {
                    string message = "Já existe um Motorista com este nome.";
                    return CommandResult.Invalid(message);
                }

                motorista.Atualizar(DataString.FromString(command.Nome),
                    DataString.FromNullableString(command.Ajudante1),
                    DataString.FromNullableString(command.Observacao),               
                    DataString.FromNullableString(command.Telefone1),
                    DataString.FromNullableString(command.Telefone2));

                //if (material.HasNotifications)
                //{                    
                //    return CommandResult.Invalid(material.Notifications.ToNotificationsString());
                //}

                dataContext.SaveChanges();
                return CommandResult.Valid();
            }
            catch (Exception ex)
            {
                return CommandResult.Invalid(ex.Message);
            }
        }

        public CommandResult Criar(CriarMotoristaCommand command)
        {
            try
            {
                command.Validate();
                if (command.Invalid)
                {
                    return CommandResult.Invalid(command.Notifications.ToNotificationsString());
                }

                Motorista motorista = Motorista.Criar(DataString.FromString(command.Nome),
                    DataString.FromNullableString(command.Ajudante1),
                    DataString.FromNullableString(command.Observacao),                  
                    DataString.FromNullableString(command.Telefone1),
                    DataString.FromNullableString(command.Telefone2));

                dataContext.Add(motorista);
                dataContext.SaveChanges();

                return CommandResult.Valid();

            }
            catch (Exception ex)
            {

                return CommandResult.Invalid(ex.Message);
            }
        }

        public CommandResult Remover(int cod_Motorista)
        {
            string entityName = "Material";
            string commandName = $"Removendo {entityName}";

            try
            {
                Motorista motorista = dataContext.Motorista.FirstOrDefault(x => x.Cod_Motorista == cod_Motorista);

                if (motorista is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada(entityName, cod_Motorista));
                }

                //if (material.HasNotifications)
                //{
                //    return CommandResult.Invalid(material.Notifications.ToNotificationsString());
                //}

                dataContext.Remove(motorista);

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
