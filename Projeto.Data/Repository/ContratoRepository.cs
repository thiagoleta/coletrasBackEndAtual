using Microsoft.EntityFrameworkCore;
using Projeto.Data.Context;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using Projeto.Data.Extensions;
using Projeto.Data.Repository.Sorts;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto.Data.Repository
{
   public class ContratoRepository : BaseRepository<Contrato>, IContratoRepository
    {
        private readonly DataColetrans dataContext;

        public ContratoRepository(DataColetrans dataContext) : base(dataContext) //construtor da classe pai..
        {
            this.dataContext = dataContext;
        }

        public override List<Contrato> Consultar()
        {
            return dataContext.Contrato              
                .Include(c => c.Cliente)
                .ToList();
        }

        public CommandResult<IReadOnlyCollection<Contrato>> Obter(ContratoSort sort, bool ascending)
        {
            var resultado = ObterBase(sort, ascending).ToArray();
            return CommandResult<IReadOnlyCollection<Contrato>>.Valid(resultado);
        }

        public CommandResult<PaginatedQueryResult<Contrato>> ObterPaginado(int pagina, int quantidade, ContratoSort sort, bool ascending)
        {
            var listaBase = ObterBase(sort, ascending);
            var total = listaBase.Count();
            var skip = Pagination.PagesToSkip(quantidade, total, pagina);

            var resultado = new PaginatedQueryResult<Contrato>()
            {
                Total = total,
                Data = listaBase.Skip(skip).Take(quantidade).ToArray()
            };

            return CommandResult<PaginatedQueryResult<Contrato>>.Valid(resultado);
        }
 

        private IQueryable<Contrato> ObterBase(ContratoSort sort, bool ascending)
        {
            IQueryable<Contrato> query = dataContext.Contrato.Include(c=> c.Cliente).AsNoTracking();


            switch (sort)
            {         

                case ContratoSort.ColetaContratada:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.ColetaContratada).ThenBy(a => a.ColetaContratada);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.ColetaContratada).ThenByDescending(a => a.ColetaContratada);
                    }
                    break;

                case ContratoSort.ValorLimite:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.ValorLimite).ThenBy(a => a.ValorLimite);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.ValorLimite).ThenByDescending(a => a.ValorLimite);
                    }
                    break;

                case ContratoSort.ValorUnidade:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.ValorUnidade).ThenBy(a => a.ValorUnidade);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.ValorUnidade).ThenByDescending(a => a.ValorUnidade);
                    }
                    break;

                case ContratoSort.FlagTermino:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.FlagTermino == null ? 0 : 1).ThenBy(a => a.FlagTermino);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.FlagTermino == null ? 0 : 1).ThenByDescending(a => a.FlagTermino);
                    }
                    break;

                case ContratoSort.DataInicio:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.DataInicio == null ? 0 : 1).ThenBy(a => a.DataInicio);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.DataInicio == null ? 0 : 1).ThenByDescending(a => a.DataInicio);
                    }
                    break;

                case ContratoSort.DataTermino:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.DataTermino == null ? 0 : 1).ThenBy(a => a.DataTermino);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.DataTermino == null ? 0 : 1).ThenByDescending(a => a.DataTermino);
                    }
                    break;


                case ContratoSort.ClienteNome:
                default:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Cliente.NomeCompleto_RazaoSocial).ThenBy(a => a.Cliente.NomeCompleto_RazaoSocial);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Cliente.NomeCompleto_RazaoSocial).ThenByDescending(a => a.Cliente.NomeCompleto_RazaoSocial);
                    }
                    break;
            }

            return query;
        }
    }
}

