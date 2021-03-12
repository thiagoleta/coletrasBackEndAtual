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
   public class PagamentoRepository : BaseRepository<Pagamento>, IPagamentoRepository
    {
        private readonly DataColetrans dataContext;

        public PagamentoRepository(DataColetrans dataContext) : base(dataContext) 
        {
            this.dataContext = dataContext;
        }

        public CommandResult<IReadOnlyCollection<Pagamento>> Obter(PagamentoSort sort, bool ascending)
        {
            var resultado = ObterBase(sort, ascending).ToArray();
            return CommandResult<IReadOnlyCollection<Pagamento>>.Valid(resultado);
        }

        public CommandResult<PaginatedQueryResult<Pagamento>> ObterPaginado(int pagina, int quantidade, PagamentoSort sort, bool ascending)
        {
            var listaBase = ObterBase(sort, ascending);
            var total = listaBase.Count();
            var skip = Pagination.PagesToSkip(quantidade, total, pagina);

            var resultado = new PaginatedQueryResult<Pagamento>()
            {
                Total = total,
                Data = listaBase.Skip(skip).Take(quantidade).ToArray()
            };

            return CommandResult<PaginatedQueryResult<Pagamento>>.Valid(resultado);
        }

        private IQueryable<Pagamento> ObterBase(PagamentoSort sort, bool ascending)
        {
            IQueryable<Pagamento> query = dataContext.Pagamento.Include(c => c.Cliente)
                                                            .Include(c => c.MesReferencia);      

            switch (sort)
            {

                case PagamentoSort.MesReferecia:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.MesReferencia.MesAno).ThenBy(a => a.MesReferencia.MesAno);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.MesReferencia.MesAno).ThenByDescending(a => a.MesReferencia.MesAno);
                    }
                    break;            

                case PagamentoSort.Valor:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Valor).ThenBy(a => a.Valor);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Valor).ThenByDescending(a => a.Valor);
                    }
                    break;

                case PagamentoSort.DataPagamento:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Data).ThenBy(a => a.Data);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Data).ThenByDescending(a => a.Data);
                    }
                    break;

                case PagamentoSort.NomeCliente:
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

