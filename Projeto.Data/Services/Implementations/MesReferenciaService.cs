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
    public class MesReferenciaService : IMesReferenciaService
    {

        private readonly DataColetrans dataContext;

        public MesReferenciaService(DataColetrans dataContext)
        {
            this.dataContext = dataContext;
        }

        public CommandResult Atualizar(AtualizarMesReferenciaCommand command)
        {

            string entityName = "MesReferencia";

            try
            {
                command.Validate();
                if (command.Invalid)
                {
                    return CommandResult.Invalid(command.Notifications.ToNotificationsString());
                }

                MesReferencia mesRef = dataContext.MesReferencia.FirstOrDefault(x => x.Cod_MesReferencia == command.Cod_MesReferencia);

                if (mesRef is null)
                {

                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada(entityName, command.Cod_MesReferencia));
                }


                mesRef.Atualizar(DataString.FromString(command.MesAno),
                    command.DataInicio,
                    command.DataTermino,
                    command.Ativo);


                dataContext.SaveChanges();
                return CommandResult.Valid();
            }
            catch (Exception ex)
            {
                return CommandResult.Invalid(ex.Message);
            }
        }


        public CommandResult Criar(CriarMesReferenciaCommand command)
        {
            try
            {
                command.Validate();
                if (command.Invalid)
                {
                    return CommandResult.Invalid(command.Notifications.ToNotificationsString());
                }

                MesReferencia mesRef = MesReferencia.Criar(
                    DataString.FromString(command.MesAno),
                    command.DataInicio,
                    command.DataTermino,
                    command.Ativo);

                dataContext.Add(mesRef);
                dataContext.SaveChanges();

                return CommandResult.Valid();

            }
            catch (Exception ex)
            {

                return CommandResult.Invalid(ex.Message);
            }
        }

        public CommandResult Remover(int cod_MesReferencia)
        {
            string entityName = "Mes Referência";
            string commandName = $"Removendo {entityName}";

            try
            {
                MesReferencia mesRef = dataContext.MesReferencia.FirstOrDefault(x => x.Cod_MesReferencia == cod_MesReferencia);

                if (mesRef is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada(entityName, cod_MesReferencia));
                }


                dataContext.Remove(mesRef);

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
