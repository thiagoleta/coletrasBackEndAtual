using Microsoft.EntityFrameworkCore;
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
    public class MaterialRepository : IMaterialRepository
    {
        private readonly DataColetrans dataContext;

        public MaterialRepository(DataColetrans dataContext)
        {
            this.dataContext = dataContext;
        }

        public CommandResult<IReadOnlyCollection<Material>> Obter(MaterialSort sort, bool ascending, string descricao)
        {         

            var resultado = ObterBase(sort, ascending, descricao).ToArray();            
            return CommandResult<IReadOnlyCollection<Material>>.Valid(resultado);
        }

        public CommandResult<PaginatedQueryResult<Material>> ObterPaginado(int pagina, int quantidade, MaterialSort sort, bool ascending, string descricao)
        {          
                       
            var listaBase = ObterBase(sort, ascending, descricao);            
            var total = listaBase.Count();            
            var skip = Pagination.PagesToSkip(quantidade, total, pagina);

            var resultado = new PaginatedQueryResult<Material>()
            {
                Total = total,
                Data = listaBase.Skip(skip).Take(quantidade).ToArray()
            };
            
            return CommandResult<PaginatedQueryResult<Material>>.Valid(resultado);
        }

        private IQueryable<Material> ObterBase(MaterialSort sort, bool ascending, string descricao)
        {
            IQueryable<Material> query = dataContext.Material.AsNoTracking();            

            switch (sort)
            {
                case MaterialSort.Material_Coletado:
                    query = query.SortBy(a => a.Material_Coletado, ascending);
                    break;


                case MaterialSort.Cod_Material:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Cod_Material == null ? 0 : 1).ThenBy(a => a.Cod_Material);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Cod_Material == null ? 0 : 1).ThenByDescending(a => a.Cod_Material);
                    }
                    break;
                case MaterialSort.Volume:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Volume == null ? 0 : 1).ThenBy(a => a.Volume);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Volume == null ? 0 : 1).ThenByDescending(a => a.Volume);
                    }
                    break;

                case MaterialSort.Observacao:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Observacao == null ? 0 : 1).ThenBy(a => a.Observacao);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Observacao == null ? 0 : 1).ThenByDescending(a => a.Observacao);
                    }
                    break;


                case MaterialSort.Descricao:
                default:
                    query = query.SortBy(x => x.Descricao, ascending);
                    break;
            }

            return query.WhereIfNotNull(x => x.Descricao.ToUpper().Contains(descricao.ToString()), descricao);
        }

    }
}


