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
    public class MaterialService : IMaterialService
    {
        private readonly DataColetrans dataContext;

        public MaterialService(DataColetrans dataContext)
        {
            this.dataContext = dataContext;
        }

        public CommandResult Atualizar(AtualizarMaterialCommand command)
        {

            string entityName = "Material";
            
            try
            {
                command.Validate();
                if (command.Invalid)
                {                    
                    return CommandResult.Invalid(command.Notifications.ToNotificationsString());
                }

                Material material = dataContext.Material.FirstOrDefault(x => x.Cod_Material == command.Cod_Material);

                if (material is null)
                {
                    
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada(entityName, command.Cod_Material));
                }


                if (dataContext.Material.Any(x => x.Descricao == command.Descricao.ToUpper() && x.Cod_Material != command.Cod_Material))
                {
                    string message = "Já existe outro Material com este nome.";                    
                    return CommandResult.Invalid(message);
                }

                material.Atualizar(DataString.FromString(command.Descricao),
                    command.Volume,
                    DataString.FromString(command.Material_Coletado),
                    DataString.FromString(command.Material_Coletado));

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

        public CommandResult Criar(CriarMaterialCommand command)
        {
            try
            {
                command.Validate();
                if (command.Invalid)
                {                    
                    return CommandResult.Invalid(command.Notifications.ToNotificationsString());
                }

                Material material = Material.Criar(DataString.FromString(command.Descricao),
                    DataString.FromNullableString(command.Volume),
                    DataString.FromNullableString(command.Material_Coletado),
                    DataString.FromNullableString(command.Material_Coletado));

                dataContext.Add(material);
                dataContext.SaveChanges();

                return CommandResult.Valid();

            }
            catch (Exception ex)
            {

                return CommandResult.Invalid(ex.Message);
            }
        }

        public CommandResult Remover(int cod_Material)
        {
            string entityName = "Material";
            string commandName = $"Removendo {entityName}";

            try
            {
                Material material = dataContext.Material.FirstOrDefault(x => x.Cod_Material == cod_Material);

                if (material is null)
                {                    
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada(entityName, cod_Material));
                }

                //if (material.HasNotifications)
                //{
                //    return CommandResult.Invalid(material.Notifications.ToNotificationsString());
                //}

                dataContext.Remove(material);

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




