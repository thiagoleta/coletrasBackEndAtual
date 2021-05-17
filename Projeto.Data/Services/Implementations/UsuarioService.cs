using Projeto.Data.Commands;
using Projeto.Data.Context;
using Projeto.Data.Entities;
using Projeto.Data.Extensions;
using Projeto.Data.Seedwork;
using Projeto.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using Projeto.Data.Util;


namespace Projeto.Data.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DataColetrans dataContext;

        public UsuarioService(DataColetrans dataContext)
        {
            this.dataContext = dataContext;
        }

        public CommandResult Atualizar(AtualizarUsuarioCommand command)
        {
            string entityName = "Usuario";
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

                var perfil = dataContext.Perfil.FirstOrDefault(x => x.Cod_Perfil == command.Cod_Perfil);

                if (perfil is null)
                {
                    string message = $"Não Existe Perfil para este usuário";
                    return CommandResult.Invalid(message);
                }


                var verificaEmail = dataContext.Usuario.FirstOrDefault(x => x.Email.Equals(command.Email));

                if (verificaEmail != null)
                {
                    string message = $"O email informado já encontra-se cadastrado";
                    return CommandResult.Invalid(message);
                }

                usuario.Atualizar(DataString.FromString(command.Nome),
                    command.Email,
                    command.Senha = Criptografia.MD5Encrypt(command.Senha), perfil);

                dataContext.SaveChanges();
                return CommandResult.Valid();
            }
            catch (Exception ex)
            {
                return CommandResult.Invalid(ex.Message);
            }
        }

        public CommandResult Criar(CriarUsuarioCommand command)
        {
            try
            {
                command.Validate();
                if (command.Invalid)
                {
                    return CommandResult.Invalid(command.Notifications.ToNotificationsString());
                }

                var perfil = dataContext.Perfil.FirstOrDefault(x=> x.Cod_Perfil == command.Cod_Perfil);

                if (perfil is null)
                {
                    string message = $"Não Existe Perfil para esta usuário";
                    return CommandResult.Invalid(message);
                }

                var verificaEmail = dataContext.Usuario.FirstOrDefault(x=> x.Email.Equals(command.Email));

                if (verificaEmail != null)
                {
                    string message = $"O email informado já encontra - se cadastrado";
                    return CommandResult.Invalid(message);
                }

                Usuario usuario = Usuario.Criar(DataString.FromString(command.Nome),
                    command.Email,
                    command.Senha = Criptografia.MD5Encrypt(command.Senha), perfil);

                dataContext.Add(usuario);
                dataContext.SaveChanges();

                return CommandResult.Valid();

            }
            catch (Exception ex)
            {
                return CommandResult.Invalid(ex.Message);
            }
        }

        public CommandResult Remover(int cod_Usuario)
        {
            string entityName = "Usuario";
            string commandName = $"Removendo {entityName}";

            try
            {
                Usuario usuario = dataContext.Usuario.FirstOrDefault(x => x.Cod_Usuario == cod_Usuario);

                if (usuario is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada(entityName, cod_Usuario));
                }            

                dataContext.Remove(usuario);
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
