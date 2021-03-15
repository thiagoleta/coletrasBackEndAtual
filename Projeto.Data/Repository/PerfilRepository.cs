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

        public override List<Perfil> Consultar()
        {
            //fazendo JOIN com a entidade Motorista
            return dataContext.Perfil
                    .Include(p => p.Usuario) //JOIN..
                    .ToList();
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

        private IQueryable<Perfil> ObterBase(PerfilSort sort, bool ascending)
        {
            IQueryable<Perfil> query = dataContext.Perfil.Include(c => c.Usuario);



            switch (sort)
            {

                case PerfilSort.UsuarioNome:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Usuario.Nome).ThenBy(a => a.Usuario.Nome);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Usuario.Nome).ThenByDescending(a => a.Usuario.Nome);
                    }
                    break;
                case PerfilSort.UsuarioEmail:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Usuario.Email).ThenBy(a => a.Usuario.Email);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Usuario.Email).ThenByDescending(a => a.Usuario.Email);
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
