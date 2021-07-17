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
    public class FrotaService : IFrotaService
    {
        private readonly DataColetrans dataContext;

        public FrotaService(DataColetrans dataContext)
        {
            this.dataContext = dataContext;
        }

        public CommandResult Atualizar(AtualizarFrotaCommand command)
        {
            string entityName = "Frota";

            try
            {
                command.Validate();
                if (command.Invalid)
                {
                    return CommandResult.Invalid(command.Notifications.ToNotificationsString());
                }

                var frota = dataContext.Frota.FirstOrDefault(f => f.Cod_Frota == command.Cod_Frota);

                if (frota is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada("Frota", command.Cod_Frota));

                }

                var motorista = dataContext.Motorista.FirstOrDefault(m => m.Cod_Motorista == command.Cod_Motorista);

                if (motorista is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada("Motorista", command.Cod_Motorista));
                }

                frota.Atualizar(motorista,
                                DataString.FromString(command.Descricao),
                                DataString.FromString(command.Placa),
                                DataString.FromNullableString(command.Observacao),
                                DataString.FromNullableString(command.Quilometragem));

                dataContext.SaveChanges();
                return CommandResult.Valid();

            }
            catch (Exception ex)
            {
                CommandResult.Invalid(ex.Message);
            }
            
            throw new NotImplementedException();
        }

        public CommandResult Criar(CriarFrotaCommand command)
        {
            try
            {                
                    command.Validate();
                    if (command.Invalid)
                    {
                        return CommandResult.Invalid(command.Notifications.ToNotificationsString());
                    }

                    var motorista = dataContext.Motorista.FirstOrDefault(m => m.Cod_Motorista == command.Cod_Motorista);

                    if (motorista is null)
                    {
                        return CommandResult.Invalid(Logs.EntidadeNaoEncontrada("Motorista", command.Cod_Motorista));
                    }

                    Frota frota = Frota.Criar(motorista,
                                               DataString.FromString(command.Descricao),
                                               DataString.FromString(command.Placa),
                                               DataString.FromNullableString(command.Observacao),
                                               DataString.FromNullableString(command.Quilometragem));

                    dataContext.Add(frota);
                    dataContext.SaveChanges();

                    return CommandResult.Valid();

                }

            catch (Exception ex)
            {
                return CommandResult.Invalid(ex.Message);
            }
            
        }

        public CommandResult Remover(int cod_Frota)
        {
            string entityName = "Frota";

            try
            {
                var frota = dataContext.Frota.FirstOrDefault(f => f.Cod_Frota == cod_Frota);

                if (frota is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada(entityName, cod_Frota));
                }

                dataContext.Remove(frota);
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
