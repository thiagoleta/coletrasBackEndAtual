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
   public class RotaRepository : BaseRepository<Rota>, IRotaRepository
    {
        private readonly DataColetrans dataContext;

        public RotaRepository(DataColetrans dataContext) : base(dataContext) 
        {
            this.dataContext = dataContext;
        }

        public CommandResult<IReadOnlyCollection<Rota>> Obter(RotaSort sort, bool ascending, string nome)
        {

            var resultado = ObterBase(sort, ascending, nome).ToArray();
            return CommandResult<IReadOnlyCollection<Rota>>.Valid(resultado);
        }

        private IQueryable<Rota> ObterBase(RotaSort sort, bool ascending, string nome)
        {
            IQueryable<Rota> query = dataContext.Rota.AsNoTracking();

            switch (sort)
            {

                case RotaSort.Cod_Rota:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Cod_Rota == null ? 0 : 1).ThenBy(a => a.Cod_Rota);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Cod_Rota == null ? 0 : 1).ThenByDescending(a => a.Cod_Rota);
                    }
                    break;

                case RotaSort.Composicao_Rota:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Composicao_Rota == null ? 0 : 1).ThenBy(a => a.Composicao_Rota);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Composicao_Rota == null ? 0 : 1).ThenByDescending(a => a.Composicao_Rota);
                    }
                    break;

                case RotaSort.Flag_Ativo:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Flag_Ativo == null ? 0 : 1).ThenBy(a => a.Flag_Ativo);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Flag_Ativo == null ? 0 : 1).ThenByDescending(a => a.Flag_Ativo);
                    }
                    break;

                case RotaSort.Observacao:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Observacao == null ? 0 : 1).ThenBy(a => a.Observacao);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Observacao == null ? 0 : 1).ThenByDescending(a => a.Observacao);
                    }
                    break;   

                case RotaSort.Nome:
                default:
                    query = query.SortBy(x => x.Nome, ascending);
                    break;
            }

            return query.WhereIfNotNull(x => x.Nome.ToUpper().Contains(x.Nome.ToString()), nome);
        }

        public CommandResult<PaginatedQueryResult<Rota>> ObterPaginado(int pagina, int quantidade, RotaSort sort, bool ascending, string nome)
        {

            var listaBase = ObterBase(sort, ascending, nome);
            var total = listaBase.Count();
            var skip = Pagination.PagesToSkip(quantidade, total, pagina);

            var resultado = new PaginatedQueryResult<Rota>()
            {
                Total = total,
                Data = listaBase.Skip(skip).Take(quantidade).ToArray()
            };

            return CommandResult<PaginatedQueryResult<Rota>>.Valid(resultado);
        }

        ////sobrescrita de método (OVERRIDE)
        //public override List<Rota> Consultar()
        //{
        //    //retornar uma consulta de Rota 
        //    //fazendo JOIN com a entidade Motorista
        //    return dataContext.Rota
        //            .Include(p => p.Motorista) //JOIN..
        //            .ToList();
        //}

        //public override Rota ObterPorId(int id)
        //{
        //    //retornar uma consulta de Rota 
        //    //fazendo JOIN com a entidade Motorista
        //    return dataContext.Rota
        //            .Include(p => p.Motorista) //JOIN..
        //            .FirstOrDefault(p => p.Cod_Rota == id);
        //}


    }
}

