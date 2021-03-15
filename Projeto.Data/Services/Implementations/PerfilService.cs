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
   public class PerfilService : IPerfilService
    {
        private readonly DataColetrans dataContext;

        public PerfilService(DataColetrans dataContext)
        {
            this.dataContext = dataContext;
        }

        public CommandResult Atualizar(AtualizarPerfilCommand command)
        {
            string entityName = "Perfil";
            try
            {
                command.Validate();
                if (command.Invalid)
                {
                    return CommandResult.Invalid(command.Notifications.ToNotificationsString());
                }

                Perfil perfil = dataContext.Perfil.FirstOrDefault(x => x.Cod_Perfil == command.Cod_Perfil);

                if (perfil is null)
                {

                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada(entityName, command.Cod_Usuario));
                }

                Usuario usuario = dataContext.Usuario.FirstOrDefault(x => x.Cod_Usuario == command.Cod_Usuario);

                if (usuario is null)
                {

                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada(entityName, command.Cod_Usuario));
                }

                perfil.Atualizar(                   
                   DataString.FromString(command.Nome_Perfil),
                   usuario);

                dataContext.SaveChanges();
                return CommandResult.Valid();
            }
            catch (Exception ex)
            {
                return CommandResult.Invalid(ex.Message);
            }
        }

        public CommandResult Criar(CriarPerfilCommand command)
        {
            string entityName = "Perfil";
            try
            {
                command.Validate();
                if (command.Invalid)
                {
                    return CommandResult.Invalid(command.Notifications.ToNotificationsString());
                }

                Usuario usuario = dataContext.Usuario.FirstOrDefault(x => x.Cod_Usuario == command.Cod_Usuario);

                if (usuario is null)
                {

                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada(entityName, command.Cod_Usuario));
                }


                Perfil perfil = Perfil.Criar(DataString.FromString(command.Nome_Perfil),
                    usuario);

                dataContext.Add(perfil);
                dataContext.SaveChanges();

                return CommandResult.Valid();

            }
            catch (Exception ex)
            {

                return CommandResult.Invalid(ex.Message);
            }
        }

        public CommandResult Remover(int cod_Perfil)
        {
            string entityName = "Perfil";
            string commandName = $"Removendo {entityName}";

            try
            {
                Perfil perfil = dataContext.Perfil.FirstOrDefault(x => x.Cod_Perfil == cod_Perfil);

                if (perfil is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada(entityName, cod_Perfil));
                }               
                dataContext.Remove(perfil);
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
