using Projeto.Data.Commands;
using Projeto.Data.Context;
using Projeto.Data.Contracts;
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
    public class ClienteService : IClienteService
    {
        private readonly DataColetrans dataContext;
        private readonly IContratoRepository contratoRepo;

        public ClienteService(DataColetrans dataContext, IContratoRepository contratoRepo)
        {
            this.dataContext = dataContext;
            this.contratoRepo = contratoRepo;
        }

        public CommandResult Atualizar(AtualizarClienteCommand command)
        {
            string entityName = "Cliente";

            try
            {
                command.Validate();
                if (command.Invalid)
                {
                    return CommandResult.Invalid(command.Notifications.ToNotificationsString());
                }

                Cliente cliente = dataContext.Cliente.FirstOrDefault(x => x.Cod_Cliente == command.Cod_Cliente);

                if (cliente is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada(entityName, command.Cod_Cliente));
                }           



                cliente.Atualizar(
                   DataString.FromString(command.CPF_CNPJ),
                   DataString.FromString(command.NomeCompleto_RazaoSocial),
                   DataString.FromString(command.Fantasia),
                   DataString.FromNullableString(command.Insc_Estadual),
                   DataString.FromNullableString(command.Logradouro),
                   DataString.FromNullableString(command.Endereco),
                   DataString.FromNullableString(command.Bairro),
                   DataString.FromNullableString(command.Complemento),
                   DataString.FromNullableString(command.Cidade),
                   DataString.FromNullableString(command.CEP),
                   DataString.FromNullableString(command.UF),
                   DataString.FromNullableString(command.Telefones),
                   DataString.FromNullableString(command.Funcao),
                   command.Flag_Ativo,
                   command.Email,
                   DataString.FromNullableString(command.Observacao),
                   DataString.FromNullableString(command.Referencia));

                dataContext.SaveChanges();
                return CommandResult.Valid();

            }
            catch (Exception ex)
            {

                return CommandResult.Invalid(ex.Message);
            }

            throw new NotImplementedException();
        }

        public CommandResult Criar(CriarClienteCommand command)
        {         

            try
            {
                command.Validate();
                if (command.Invalid)
                {                    
                    return CommandResult.Invalid(command.Notifications.ToNotificationsString());
                }   


                Cliente cliente = Cliente.Criar(
                        DataString.FromString(command.CPF_CNPJ),
                        DataString.FromString(command.NomeCompleto_RazaoSocial),
                        DataString.FromString(command.Fantasia),
                        DataString.FromNullableString(command.Insc_Estadual),
                        DataString.FromNullableString(command.Logradouro),
                        DataString.FromNullableString(command.Endereco),
                        DataString.FromNullableString(command.Bairro),
                        DataString.FromNullableString(command.Complemento),
                        DataString.FromNullableString(command.Cidade),
                        DataString.FromNullableString(command.CEP),
                        DataString.FromNullableString(command.UF),
                        DataString.FromNullableString(command.Telefones),
                        DataString.FromNullableString(command.Funcao),
                        command.Flag_Ativo,
                        command.Email,
                        DataString.FromNullableString(command.Observacao),
                        DataString.FromNullableString(command.Referencia));          

         
                dataContext.Add(cliente);
                dataContext.SaveChanges();
                

                return CommandResult.Valid();
            }
            catch (Exception ex)
            {
                
                return CommandResult.Invalid(ex.Message);
            }
        }


        public CommandResult Remover(int cod_Cliente)
        {
            string entityName = "Cliente";
            string commandName = $"Removendo {entityName}";

            try
            {
                Cliente cliente = dataContext.Cliente.FirstOrDefault(x => x.Cod_Cliente == cod_Cliente);

                if (cliente is null)
                {
                    return CommandResult.Invalid(Logs.EntidadeNaoEncontrada(entityName, cod_Cliente));
                }

                Contrato contrato = dataContext.Contrato.FirstOrDefault(x=> x.Cod_Cliente == cod_Cliente && x.Flag_Termino.Equals(true));

                if (contrato != null)
                {
                    string message = "Existe um contrato Ativo para este Cliente, Exclusão não pode ser realizada. ";
                    return CommandResult.Invalid(message);
                }
     

                dataContext.Remove(cliente);

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
