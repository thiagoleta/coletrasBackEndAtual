using Dapper;
using Projeto.Data.Context;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Projeto.Data.Seedwork;
using Projeto.Data.Extensions;
using Projeto.Data.Repository.Sorts;
using Microsoft.EntityFrameworkCore;

namespace Projeto.Data.Repository.RepositoriosDapper
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly DataColetrans dataContext;
        //private readonly string connectionString;

        //public UsuarioRepository(string connectionString)
        //{
        //    this.connectionString = connectionString;
        //}

        public UsuarioRepository(DataColetrans dataContext)
        {
            this.dataContext = dataContext;
        }

        //public void Inserir(Usuario entity)
        //{
        //    var query = "insert into Usuario(Nome, Email, Senha, DataCriacao) "
        //              + "values(@Nome, @Email, @Senha, @DataCriacao)";

        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        connection.Execute(query, entity);
        //    }
        //}

        //public void Atualizar(Usuario entity)
        //{
        //    var query = "update Usuario set Nome = @Nome, Email = @Email, Senha = @Senha "
        //              + "where Cod_Usuario = @Cod_Usuario";

        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        connection.Execute(query, entity);
        //    }
        //}

        //public void Excluir(Usuario entity)
        //{
        //    var query = "delete from Usuario where Cod_Usuario = @Cod_Usuario";

        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        connection.Execute(query, entity);
        //    }
        //}

        //public List<Usuario> ConsultarTodos()
        //{
        //    var query = "select * from Usuario";

        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        return connection.Query<Usuario>(query).ToList();
        //    }
        //}
        //public Usuario ObterPorId(int id)
        //{
        //    var query = "select * from Usuario where Cod_Usuario = @Cod_Usuario";

        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        return connection.Query<Usuario>
        //        (query, new { Cod_Usuario = id }).FirstOrDefault();
        //    }
        //}

        //public Usuario Obter(string email)
        //{
        //    var query = "select * from Usuario where Email = @Email";

        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        return connection.Query<Usuario>
        //        (query, new { Email = email }).FirstOrDefault();
        //    }
        //}

        //public Usuario Obter(string email, string senha)
        //{
        //    var query = "select * from Usuario where Email = @Email and Senha = @Senha";

        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        return connection.Query<Usuario>
        //            (query, new
        //            {
        //                Email = email,
        //                Senha = senha
        //            }).FirstOrDefault();
        //    }
        //}

        public CommandResult<PaginatedQueryResult<Usuario>> ObterPaginado(int pagina, int quantidade, UsuarioSort sort, bool ascending)
        {
            var listaBase = ObterBase(sort, ascending);
            var total = listaBase.Count();
            var skip = Pagination.PagesToSkip(quantidade, total, pagina);

            var resultado = new PaginatedQueryResult<Usuario>()
            {
                Total = total,
                Data = listaBase.Skip(skip).Take(quantidade).ToArray()
            };

            return CommandResult<PaginatedQueryResult<Usuario>>.Valid(resultado);
        }

        public CommandResult<IReadOnlyCollection<Usuario>> ObterUsuarios(UsuarioSort sort, bool ascending)
        {
            var resultado = ObterBase(sort, ascending).ToArray();
            return CommandResult<IReadOnlyCollection<Usuario>>.Valid(resultado);
        }



        private IQueryable<Usuario> ObterBase(UsuarioSort sort, bool ascending)
        {

            IQueryable<Usuario> query = dataContext.Usuario.Include(x=> x.Perfil);

            switch (sort)
            {

                case UsuarioSort.Email:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Email).ThenBy(a => a.Email);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Email).ThenByDescending(a => a.Email);
                    }
                    break;
                case UsuarioSort.Perfil:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Perfil.Nome_Perfil).ThenBy(a => a.Perfil.Nome_Perfil);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Perfil.Nome_Perfil).ThenByDescending(a => a.Perfil.Nome_Perfil);
                    }
                    break;
                case UsuarioSort.Nome:
                default:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Nome).ThenBy(a => a.Nome);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Nome).ThenByDescending(a => a.Nome);
                    }
                    break;
            }

            return query;
        }

        public CommandResult<IReadOnlyCollection<Usuario>> ObterUsuariosSelect()
        {
            IQueryable<Usuario> query = dataContext.Usuario.AsNoTracking();
            var result = query.OrderBy(x => x.Nome).Select(x => new Usuario(x.Cod_Usuario, x.Nome)).ToArray();
            return CommandResult<IReadOnlyCollection<Usuario>>.Valid(result);
        }

        public Usuario Obter(string email, string senha)
        {
            var msg = "Usuário não localizado no Sistema, não foi poissível Logar.";
            var consulta = dataContext.Usuario.Where(x=> x.Email.Equals(email) && x.Senha.Equals(senha)).FirstOrDefault();      

            return consulta;
        }
      
    }
   
}
