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
   public class MesReferenciaRepository : BaseRepository<MesReferencia>, IMesReferenciaRepository
    {
        private readonly DataColetrans dataContext;

        public MesReferenciaRepository(DataColetrans dataContext) : base(dataContext) //construtor da classe pai..
        {
            this.dataContext = dataContext;
        }

        public CommandResult<IReadOnlyCollection<MesReferencia>> Obter(MesReferenciaSort sort, bool ascending, string mesAno)
        {
            var resultado = ObterBase(sort, ascending, mesAno).ToArray();
            return CommandResult<IReadOnlyCollection<MesReferencia>>.Valid(resultado);
        }

        public CommandResult<IReadOnlyCollection<MesReferencia>> ObterMesRefAtivos()
        {
            IQueryable<MesReferencia> query = dataContext.MesReferencia.AsNoTracking();
            var result = query.OrderBy(x => x.MesAno).Select(x => new MesReferencia(x.Cod_MesReferencia, x.MesAno)).ToArray();
            return CommandResult<IReadOnlyCollection<MesReferencia>>.Valid(result);
        }

        public CommandResult<PaginatedQueryResult<MesReferencia>> ObterPaginado(int pagina, int quantidade, MesReferenciaSort sort, bool ascending, string mesAno)
        {

            var listaBase = ObterBase(sort, ascending, mesAno);
            var total = listaBase.Count();
            var skip = Pagination.PagesToSkip(quantidade, total, pagina);

            var resultado = new PaginatedQueryResult<MesReferencia>()
            {
                Total = total,
                Data = listaBase.Skip(skip).Take(quantidade).ToArray()
            };

            return CommandResult<PaginatedQueryResult<MesReferencia>>.Valid(resultado);
        }

        private IQueryable<MesReferencia> ObterBase(MesReferenciaSort sort, bool ascending, string mesAno)
        {
            IQueryable<MesReferencia> query = dataContext.MesReferencia.AsNoTracking();

            switch (sort)
            {
                case MesReferenciaSort.DataInicioMesReferencia:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.DataInicio == null ? 0 : 1).ThenBy(a => a.DataInicio);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.DataInicio == null ? 0 : 1).ThenByDescending(a => a.DataInicio);
                    }
                    break;


                case MesReferenciaSort.DataFinalMesReferencia:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.DataTermino == null ? 0 : 1).ThenBy(a => a.DataTermino);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.DataTermino == null ? 0 : 1).ThenByDescending(a => a.DataTermino);
                    }
                    break;              


                case MesReferenciaSort.MesAno:
                default:
                    query = query.SortBy(x => x.MesAno, ascending);
                    break;
            }

            return query.WhereIfNotNull(x => x.MesAno.ToUpper().Contains(mesAno.ToString()), mesAno);
        }
    }
}

