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
  public class PerfilRepository : BaseRepository<Perfil>, IPerfilRepository
    {
        private readonly DataColetrans dataContext;

        public PerfilRepository(DataColetrans dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }
        public CommandResult<IReadOnlyCollection<Perfil>> Obter(PerfilSort sort, bool ascending)
        {
            var resultado = ObterBase(sort, ascending).ToArray();
            return CommandResult<IReadOnlyCollection<Perfil>>.Valid(resultado);
        }

        public CommandResult<PaginatedQueryResult<Perfil>> ObterPaginado(int pagina, int quantidade, PerfilSort sort, bool ascending)
        {
            var listaBase = ObterBase(sort, ascending);
            var total = listaBase.Count();
            var skip = Pagination.PagesToSkip(quantidade, total, pagina);

            var resultado = new PaginatedQueryResult<Perfil>()
            {
                Total = total,
                Data = listaBase.Skip(skip).Take(quantidade).ToArray()
            };

            return CommandResult<PaginatedQueryResult<Perfil>>.Valid(resultado);
        }

        public CommandResult<IReadOnlyCollection<Perfil>> ObterPerfil()
        {
            IQueryable<Perfil> query = dataContext.Perfil.AsNoTracking();            
            var result = query.OrderBy(x => x.Nome_Perfil).Select(x => new Perfil(x.Cod_Perfil, x.Nome_Perfil)).ToArray();
            return CommandResult<IReadOnlyCollection<Perfil>>.Valid(result);
        }

        private IQueryable<Perfil> ObterBase(PerfilSort sort, bool ascending)
        {
            IQueryable<Perfil> query = dataContext.Perfil.AsNoTracking();

          
            switch (sort)
            {

                case PerfilSort.Cod_Perfil:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Cod_Perfil).ThenBy(a => a.Cod_Perfil);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Cod_Perfil).ThenByDescending(a => a.Cod_Perfil);
                    }
                    break;                
                case PerfilSort.NomePerfil:
                default:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Nome_Perfil).ThenBy(a => a.Nome_Perfil);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Nome_Perfil).ThenByDescending(a => a.Nome_Perfil);
                    }
                    break;
            }

            return query;
        }
    }
}
