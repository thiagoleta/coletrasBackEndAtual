using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Projeto.Data.Context;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using Projeto.Data.Extensions;
using Projeto.Data.Repository.Sorts;
using Projeto.Data.Seedwork;
using Projeto.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto.Data.Repository
{
   public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        private readonly DataColetrans dataContext;

        public ClienteRepository(DataColetrans dataContext) : base(dataContext) //construtor da classe pai..
        {
            this.dataContext = dataContext;
        }

        public CommandResult<IReadOnlyCollection<Cliente>> Obter(ClienteSort sort, bool ascending, DataString? nomeCompleto_RazaoSocial, DataString? cpfCnpj)
        {

            
            var resultado = ObterBase(sort, ascending, nomeCompleto_RazaoSocial, cpfCnpj).ToArray();
            
            return CommandResult<IReadOnlyCollection<Cliente>>.Valid(resultado);
        }

        public CommandResult<IReadOnlyCollection<Cliente>> ObterClientesAtivos()
        {   
            IQueryable<Cliente> query = dataContext.Cliente.AsNoTracking();   
            var result = query.OrderBy(x => x.NomeCompleto_RazaoSocial).Select(x => new Cliente(x.Cod_Cliente, x.NomeCompleto_RazaoSocial)).ToArray();            
            return CommandResult<IReadOnlyCollection<Cliente>>.Valid(result);
        }

        public CommandResult<PaginatedQueryResult<Cliente>> ObterPaginado(ClienteSort sort, bool ascending, int pagina, int quantidade, DataString? nomeCompleto_RazaoSocial, DataString? cpfCnpj)
        {

            
            var listaBase = ObterBase(sort, ascending, nomeCompleto_RazaoSocial, cpfCnpj);            
            var total = listaBase.Count();            
            var skip = Pagination.PagesToSkip(quantidade, total, pagina);

            var resultado = new PaginatedQueryResult<Cliente>()
            {
                Total = total,
                Data = listaBase.Skip(skip).Take(quantidade).ToArray()
            };
            
            return CommandResult<PaginatedQueryResult<Cliente>>.Valid(resultado);
        }

        private IQueryable<Cliente> ObterBase(ClienteSort sort, bool ascending, DataString? nomeCompleto_RazaoSocial, DataString? cpfCnpj)
        
            {

            IQueryable<Cliente> query = dataContext.Cliente;
            switch (sort)
            {
             
                case ClienteSort.Cod_Cliente:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Cod_Cliente.Equals(null) ? 0 : 1).ThenBy(a => a.Cod_Cliente);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Cod_Cliente.Equals(null) ? 0 : 1).ThenByDescending(a => a.Cod_Cliente);
                    }
                    break;
                case ClienteSort.CPF_CNPJ:
                    {
                        query = ascending ? query.OrderBy(x => x.CPF_CNPJ == null ? 0 : 1).ThenBy(x => x.CPF_CNPJ) :
                        query = query.OrderByDescending(x => x.CPF_CNPJ == null ? 0 : 1).ThenByDescending(x => x.CPF_CNPJ);
                        break;
                    }              
                case ClienteSort.Fantasia:
                    {
                        query = ascending ? query.OrderBy(x => x.Fantasia == null ? 0 : 1).ThenBy(x => x.Fantasia) :
                        query = query.OrderByDescending(x => x.Fantasia == null ? 0 : 1).ThenByDescending(x => x.Fantasia);
                        break;
                    }
                case ClienteSort.Insc_Estadual:
                    {
                        query = ascending ? query.OrderBy(x => x.Insc_Estadual == null ? 0 : 1).ThenBy(x => x.Insc_Estadual) :
                        query = query.OrderByDescending(x => x.Insc_Estadual == null ? 0 : 1).ThenByDescending(x => x.Insc_Estadual);
                        break;
                    }
                case ClienteSort.Endereco:
                    {
                        query = ascending ? query.OrderBy(x => x.Endereco == null ? 0 : 1).ThenBy(x => x.Endereco) :
                        query = query.OrderByDescending(x => x.Endereco == null ? 0 : 1).ThenByDescending(x => x.Endereco);
                        break;
                    }
                case ClienteSort.Bairro:
                    {
                        query = ascending ? query.OrderBy(x => x.Bairro == null ? 0 : 1).ThenBy(x => x.Bairro) :
                        query = query.OrderByDescending(x => x.Bairro == null ? 0 : 1).ThenByDescending(x => x.Bairro);
                        break;
                    }
                case ClienteSort.CEP:
                    {
                        query = ascending ? query.OrderBy(x => x.CEP == null ? 0 : 1).ThenBy(x => x.CEP) :
                        query = query.OrderByDescending(x => x.CEP == null ? 0 : 1).ThenByDescending(x => x.CEP);
                        break;
                    }
                    //que vem no front
                case ClienteSort.Telefones:
                    {
                        query = ascending ? query.OrderBy(x => x.Telefones == null ? 0 : 1).ThenBy(x => x.Telefones) :
                        query = query.OrderByDescending(x => x.Telefones == null ? 0 : 1).ThenByDescending(x => x.Telefones);
                        break;
                    }
                case ClienteSort.Email:
                    {
                        query = ascending ? query.OrderBy(x => x.Email == null ? 0 : 1).ThenBy(x => x.Email) :
                        query = query.OrderByDescending(x => x.Email == null ? 0 : 1).ThenByDescending(x => x.Email);
                        break;
                    }
                case ClienteSort.Flag_Ativo:
                    {
                        query = ascending ? query.OrderBy(x => x.Flag_Ativo == null ? 0 : 1).ThenBy(x => x.Flag_Ativo) :
                        query = query.OrderByDescending(x => x.Flag_Ativo == null ? 0 : 1).ThenByDescending(x => x.Flag_Ativo);
                        break;
                    }
                case ClienteSort.Observacao:
                    {
                        query = ascending ? query.OrderBy(x => x.Observacao == null ? 0 : 1).ThenBy(x => x.Observacao) :
                        query = query.OrderByDescending(x => x.Observacao == null ? 0 : 1).ThenByDescending(x => x.Observacao);
                        break;
                    }
                case ClienteSort.Referencia:
                    {
                        query = ascending ? query.OrderBy(x => x.Referencia == null ? 0 : 1).ThenBy(x => x.Referencia) :
                        query = query.OrderByDescending(x => x.Referencia == null ? 0 : 1).ThenByDescending(x => x.Referencia);
                        break;
                    }
                default:
                    query = ascending ? query.OrderBy(x => x.NomeCompleto_RazaoSocial == null ? 0 : 1).ThenBy(x => x.NomeCompleto_RazaoSocial) :
                    query = query.OrderByDescending(x => x.NomeCompleto_RazaoSocial == null ? 0 : 1).ThenByDescending(x => x.NomeCompleto_RazaoSocial);
                    break;
            }
            return query
                 .AsNoTracking()
                .WhereIfNotNull(x => x.NomeCompleto_RazaoSocial.ToUpper().Contains(nomeCompleto_RazaoSocial.ToString()), nomeCompleto_RazaoSocial)
                .WhereIfNotNull(x => x.CPF_CNPJ.ToUpper().Contains(cpfCnpj.ToString()), cpfCnpj);
        }
    }
}

