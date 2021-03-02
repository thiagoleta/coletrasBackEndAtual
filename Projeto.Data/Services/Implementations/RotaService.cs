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
   public class RotaService : IRotaService
    {
        private readonly DataColetrans dataContext;

        public RotaService(DataColetrans dataContext)
        {
            this.dataContext = dataContext;
        }

        public CommandResult Atualizar(AtualizarRotaCommand command)
        {

            string entityName = "Rota";
            string commandName = $"Atualizando {entityName}";
            try
            {
                command.Validate();
                if (command.Invalid)
                {
                    return CommandResult.Invalid(command.Notifications.ToNotificationsString());
                }

                Rota rota = dataContext.Rota.FirstOrDefault(x => x.Cod_Rota == command.Cod_Rota);

                if (rota is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada(entityName, command.Cod_Rota));
                }

                if (dataContext.Rota.Any(x => x.Nome == command.Nome.ToUpper() && x.Cod_Rota != command.Cod_Rota))
                {
                    string message = "Já existe uma Rota com este nome.";
                    return CommandResult.Invalid(message);
                }

                rota.Atualizar(DataString.FromString(command.Nome),
                    DataString.FromNullableString(command.Composicao_Rota),
                    DataString.FromNullableString(command.Observacao),
                    command.Flag_Ativo);


                dataContext.SaveChanges();
                return CommandResult.Valid();
            }
            catch (Exception ex)
            {
                return CommandResult.Invalid(ex.Message);
            }
        }


        public CommandResult Criar(CriarRotaCommand command)
        {
            try
            {
                command.Validate();
                if (command.Invalid)
                {
                    return CommandResult.Invalid(command.Notifications.ToNotificationsString());
                }

                Rota rota = Rota.Criar(
                    DataString.FromString(command.Nome),
                    DataString.FromNullableString(command.Composicao_Rota),
                    DataString.FromNullableString(command.Observacao),                               
                    command.Flag_Ativo);

                dataContext.Add(rota);
                dataContext.SaveChanges();

                return CommandResult.Valid();

            }
            catch (Exception ex)
            {

                return CommandResult.Invalid(ex.Message);
            }
        }


        public CommandResult Remover(int cod_Rota)
        {
            string entityName = "Rota";
            string commandName = $"Removendo {entityName}";

            try
            {
                Rota rota = dataContext.Rota.FirstOrDefault(x => x.Cod_Rota == cod_Rota);

                if (rota is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada(entityName, cod_Rota));
                }
          

                dataContext.Remove(rota);

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
