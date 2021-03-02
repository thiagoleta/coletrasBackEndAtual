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
   public class RoteiroRepository : BaseRepository<Roteiro>, IRoteiroRepository
    {
        private readonly DataColetrans dataContext;

        public RoteiroRepository(DataColetrans dataContext) : base(dataContext) //construtor da classe pai..
        {
            this.dataContext = dataContext;
        }

        public CommandResult<IReadOnlyCollection<Roteiro>> Obter(RoteiroSort sort, bool ascending)
        {
            var resultado = ObterBase(sort, ascending).ToArray();
            return CommandResult<IReadOnlyCollection<Roteiro>>.Valid(resultado);
        }

        public CommandResult<PaginatedQueryResult<Roteiro>> ObterPaginado(int pagina, int quantidade, RoteiroSort sort, bool ascending)
        {
            var listaBase = ObterBase(sort, ascending);
            var total = listaBase.Count();
            var skip = Pagination.PagesToSkip(quantidade, total, pagina);

            var resultado = new PaginatedQueryResult<Roteiro>()
            {
                Total = total,
                Data = listaBase.Skip(skip).Take(quantidade).ToArray()
            };

            return CommandResult<PaginatedQueryResult<Roteiro>>.Valid(resultado);
        }
  

        private IQueryable<Roteiro> ObterBase(RoteiroSort sort, bool ascending)
        {
            IQueryable<Roteiro> query = dataContext.Roteiro.Include(c => c.Cliente)
                                                            .Include(c => c.Turno)
                                                            .Include(c => c.Rota)
                                                             .Include(c => c.Motorista)
                                                             .Include(c => c.Material);
                                                              


            switch (sort)
            {        

                case RoteiroSort.Turno:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Turno.Nome_Turno).ThenBy(a => a.Turno.Nome_Turno);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Turno.Nome_Turno).ThenByDescending(a => a.Turno.Nome_Turno);
                    }
                    break;

                case RoteiroSort.Motorista:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Motorista.Nome).ThenBy(a => a.Motorista.Nome);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Motorista.Nome).ThenByDescending(a => a.Motorista.Nome);
                    }
                    break;
                case RoteiroSort.Rota:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Rota.Nome).ThenBy(a => a.Rota.Nome);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Rota.Nome).ThenByDescending(a => a.Rota.Nome);
                    }
                    break;

                case RoteiroSort.Material:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Material.Descricao).ThenBy(a => a.Material.Descricao);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Material.Descricao).ThenByDescending(a => a.Material.Descricao);
                    }
                    break;

                case RoteiroSort.Segunda:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Segunda).ThenBy(a => a.Segunda);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Segunda).ThenByDescending(a => a.Segunda);
                    }
                    break;

                case RoteiroSort.Terca:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Terca).ThenBy(a => a.Terca);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Terca).ThenByDescending(a => a.Terca);
                    }
                    break;

                case RoteiroSort.Quarta:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Quarta).ThenBy(a => a.Quarta);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Quarta).ThenByDescending(a => a.Quarta);
                    }
                    break;

                case RoteiroSort.Quinta:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Quinta).ThenBy(a => a.Quinta);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Quinta).ThenByDescending(a => a.Quinta);
                    }
                    break;

                case RoteiroSort.Sexta:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Sexta).ThenBy(a => a.Sexta);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Sexta).ThenByDescending(a => a.Sexta);
                    }
                    break;

                case RoteiroSort.Sabado:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Sabado).ThenBy(a => a.Sabado);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Sabado).ThenByDescending(a => a.Sabado);
                    }
                    break;

                case RoteiroSort.Domingo:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Domingo).ThenBy(a => a.Domingo);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Domingo).ThenByDescending(a => a.Domingo);
                    }
                    break;

                case RoteiroSort.Cliente:
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
