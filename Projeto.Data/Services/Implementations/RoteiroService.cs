using Microsoft.Extensions.Logging;
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
    public class RoteiroService : IRoteiroService
    {
        private readonly DataColetrans dataContext;

        public RoteiroService(DataColetrans dataContext)
        {
            this.dataContext = dataContext;
        }

        public CommandResult Atualizar(AtualizarRoteiroCommand command)
        {

            string entityName = "Roteiro";
            string commandName = $"Atualizando {entityName}";
            try
            {
                command.Validate();
                if (command.Invalid)
                {
                    return CommandResult.Invalid(command.Notifications.ToNotificationsString());
                }

                var roteiro = dataContext.Roteiro.FirstOrDefault(x => x.Cod_Roteiro == command.Cod_Roteiro);

                if (roteiro is null)
                {                    
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada("Roteiro", command.Cod_Roteiro));
                }

                var cliente = dataContext.Cliente.FirstOrDefault(x => x.Cod_Cliente == command.Cod_Cliente);

                if (cliente is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada("Cliente", command.Cod_Cliente));
                }

                var turno = dataContext.Turno.FirstOrDefault(x => x.Cod_Turno == command.Cod_Turno);

                if (turno is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada("Turno", (int)command.Cod_Turno));
                }

                var rota = dataContext.Rota.FirstOrDefault(x => x.Cod_Rota == command.Cod_Rota);

                if (rota is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada("Rota", command.Cod_Rota));
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

                roteiro.Atualizar(cliente,
                                   turno,
                                   rota,
                                   motorista,
                                   material,
                                   command.Segunda,
                                   command.Terca,
                                   command.Quarta,
                                   command.Quinta,
                                   command.Sexta,
                                   command.Sabado,
                                   command.Domingo,
                                   DataString.FromNullableString(command.Observacao));        

                dataContext.SaveChanges();
                return CommandResult.Valid();
            }
            catch (Exception ex)
            {
                return CommandResult.Invalid(ex.Message);
            }
        }

        public CommandResult Criar(CriarRoteiroCommand command)
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

                var turno = dataContext.Turno.FirstOrDefault(x => x.Cod_Turno == command.Cod_Turno);

                if (turno is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada("Turno", (int)command.Cod_Turno));
                }

                var rota = dataContext.Rota.FirstOrDefault(x => x.Cod_Rota == command.Cod_Rota);

                if (rota is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada("Rota", command.Cod_Rota));
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

                Roteiro roteiro = Roteiro.Criar(cliente,
                                                   turno,
                                                   rota,
                                                   motorista,
                                                   material,
                                                   command.Segunda,
                                                   command.Terca,
                                                   command.Quarta,
                                                   command.Quinta,
                                                   command.Sexta,
                                                   command.Sabado,
                                                   command.Domingo,
                                                   DataString.FromNullableString(command.Observacao));

                dataContext.Add(roteiro);
                dataContext.SaveChanges();

                return CommandResult.Valid();

            }
            catch (Exception ex)
            {

                return CommandResult.Invalid(ex.Message);
            }
        }

        public CommandResult Remover(int cod_Roteiro)
        {
            string entityName = "Roteiro";
            string commandName = $"Removendo {entityName}";

            try
            {
                Roteiro roteiro = dataContext.Roteiro.FirstOrDefault(x => x.Cod_Roteiro == cod_Roteiro);

                if (roteiro is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada(entityName, cod_Roteiro));
                }


                dataContext.Remove(roteiro);

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
