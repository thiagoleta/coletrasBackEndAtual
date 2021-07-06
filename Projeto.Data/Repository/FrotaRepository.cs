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
   public class FrotaRepository : IFrotaRepository
    {
        private readonly DataColetrans dataContext;

        public FrotaRepository(DataColetrans dataContext)
        {
            this.dataContext = dataContext;
        }

        public CommandResult<IReadOnlyCollection<Frota>> Obter(FrotaSort sort, bool ascending, string descricao)
        {

            var resultado = ObterBase(sort, ascending, descricao).ToArray();
            return CommandResult<IReadOnlyCollection<Frota>>.Valid(resultado);
        }

        public CommandResult<IReadOnlyCollection<Frota>> ObterFrota()
        {
            IQueryable<Frota> query = dataContext.Frota.AsNoTracking();
            var result = query.OrderBy(x => x.Placa).Select(x => new Frota(x.Cod_Frota, x.Placa)).ToArray();
            return CommandResult<IReadOnlyCollection<Frota>>.Valid(result);
        }

        public CommandResult<PaginatedQueryResult<Frota>> ObterPaginado(int pagina, int quantidade, FrotaSort sort, bool ascending, string descricao)
        {
            var listaBase = ObterBase(sort, ascending, descricao);
            var total = listaBase.Count();
            var skip = Pagination.PagesToSkip(quantidade, total, pagina);

            var resultado = new PaginatedQueryResult<Frota>()
            {
                Total = total,
                Data = listaBase.Skip(skip).Take(quantidade).ToArray()
            };

            return CommandResult<PaginatedQueryResult<Frota>>.Valid(resultado);
        }

        private IQueryable<Frota> ObterBase(FrotaSort sort, bool ascending, string descricao)
        {
            IQueryable<Frota> query = dataContext.Frota.Include(f => f.Motorista).AsNoTracking();

            switch (sort)
            {
                case FrotaSort.IdFrota:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Cod_Frota == null ? 0 : 1).ThenBy(a => a.Cod_Frota);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Cod_Frota == null ? 0 : 1).ThenByDescending(a => a.Cod_Frota);
                    }
                    break;
                case FrotaSort.Placa:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Placa == null ? 0 : 1).ThenBy(a => a.Placa);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Placa == null ? 0 : 1).ThenByDescending(a => a.Placa);
                    }
                    break;
                case FrotaSort.kM:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.KM == null ? 0 : 1).ThenBy(a => a.Observacao);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.KM == null ? 0 : 1).ThenByDescending(a => a.KM);
                    }
                    break;



                case FrotaSort.DescricaoFrota:
                default:
                    query = query.SortBy(x => x.Descricao, ascending);
                    break;
            }

            return query.WhereIfNotNull(x => x.Descricao.ToUpper().Contains(descricao.ToString()), descricao);
        }
    }
}
