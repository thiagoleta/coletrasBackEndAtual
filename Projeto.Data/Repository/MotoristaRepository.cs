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
   public class MotoristaRepository : BaseRepository<Motorista>, IMotoristaRepository
    {
        private readonly DataColetrans dataContext;

        public MotoristaRepository(DataColetrans dataContext) : base(dataContext) //construtor da classe pai..
        {
            this.dataContext = dataContext;
        }

        public CommandResult<IReadOnlyCollection<Motorista>> Obter(MotoristaSort sort, bool ascending, string nome)
        {
            var resultado = ObterBase(sort, ascending, nome).ToArray();
            return CommandResult<IReadOnlyCollection<Motorista>>.Valid(resultado);
        }

        public CommandResult<IReadOnlyCollection<Motorista>> ObterMotoristas()
        {
            IQueryable<Motorista> query = dataContext.Motorista.AsNoTracking();
            var result = query.OrderBy(x => x.Nome).Select(x => new Motorista(x.Cod_Motorista, x.Nome)).ToArray();            
            return CommandResult<IReadOnlyCollection<Motorista>>.Valid(result);
        }

        public CommandResult<PaginatedQueryResult<Motorista>> ObterPaginado(int pagina, int quantidade, MotoristaSort sort, bool ascending, string nome)
        {

            var listaBase = ObterBase(sort, ascending, nome);
            var total = listaBase.Count();
            var skip = Pagination.PagesToSkip(quantidade, total, pagina);

            var resultado = new PaginatedQueryResult<Motorista>()
            {
                Total = total,
                Data = listaBase.Skip(skip).Take(quantidade).ToArray()
            };

            return CommandResult<PaginatedQueryResult<Motorista>>.Valid(resultado);
        }

        private IQueryable<Motorista> ObterBase(MotoristaSort sort, bool ascending, string nome)
        {
            IQueryable<Motorista> query = dataContext.Motorista.AsNoTracking();

            switch (sort)
            {

                case MotoristaSort.Cod_Motorista:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Cod_Motorista == null ? 0 : 1).ThenBy(a => a.Cod_Motorista);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Cod_Motorista == null ? 0 : 1).ThenByDescending(a => a.Cod_Motorista);
                    }
                    break;

                case MotoristaSort.Ajudante1:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Ajudante1 == null ? 0 : 1).ThenBy(a => a.Ajudante1);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Ajudante1 == null ? 0 : 1).ThenByDescending(a => a.Ajudante1);
                    }
                    break;

                case MotoristaSort.Ajudante2:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Ajudante2 == null ? 0 : 1).ThenBy(a => a.Ajudante2);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Ajudante2 == null ? 0 : 1).ThenByDescending(a => a.Ajudante2);
                    }
                    break;     

                case MotoristaSort.Telefone1:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Telefone1 == null ? 0 : 1).ThenBy(a => a.Telefone1);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Telefone1 == null ? 0 : 1).ThenByDescending(a => a.Telefone1);
                    }
                    break;

                case MotoristaSort.Telefone2:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Telefone2 == null ? 0 : 1).ThenBy(a => a.Telefone2);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Telefone2 == null ? 0 : 1).ThenByDescending(a => a.Telefone2);
                    }
                    break;


                case MotoristaSort.Placa:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Placa == null ? 0 : 1).ThenBy(a => a.Placa);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Placa == null ? 0 : 1).ThenByDescending(a => a.Placa);
                    }
                    break;


                case MotoristaSort.Nome:
                default:
                    query = query.SortBy(x => x.Nome, ascending);
                    break;
            }

            return query.WhereIfNotNull(x => x.Nome.ToUpper().Contains(x.Nome.ToString()), nome);
        }
        #region MyRegion
        ////sobrescrita de método (OVERRIDE)
        //public override List<Motorista> Consultar()
        //{
        //    //retornar uma consulta de Motorista 
        //    //fazendo JOIN com a entidade Rotas
        //    return dataContext.Motorista
        //            .Include(p => p.Rota) //JOIN..
        //            .ToList();
        //}

        ////sobrescrita de método (OVERRIDE)
        //public override List<Motorista> Consultar(Func<Motorista, bool> where)
        //{
        //    //retornar uma consulta de Motorista 
        //    //JOIN com a entidade Rotas
        //    return dataContext.Motorista
        //            .Include(p => p.Rota) //JOIN..
        //            .Where(where)
        //            .ToList();
        //}

        ////sobrescrita de método (OVERRIDE)
        //public override Motorista Obter(Func<Motorista, bool> where)
        //{
        //    //retornar uma consulta de Produtos fazendo 
        //    //JOIN com a entidade Rotas
        //    return dataContext.Motorista
        //            .Include(p => p.Rota) //JOIN..
        //            .Where(where)
        //            .FirstOrDefault();
        //}

        ////sobrescrita de método (OVERRIDE)
        //public override Motorista ObterPorId(int id)
        //{
        //    //retornar uma consulta de Produtos fazendo 
        //    //JOIN com a entidade Rotas
        //    return dataContext.Motorista
        //            .Include(p => p.Rota) //JOIN..
        //            .FirstOrDefault(p => p.CodMotorista == id);
        //}
        #endregion
    }
}
