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

namespace Projeto.Data.Repository.RepositoriosDapper
{
    public class UsuarioRepository : IUsuarioRepository
    {


        private readonly string connectionString;

        public UsuarioRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Inserir(Usuario entity)
        {
            var query = "insert into Usuario(Nome, Email, Senha, DataCriacao) "
                      + "values(@Nome, @Email, @Senha, @DataCriacao)";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Atualizar(Usuario entity)
        {
            var query = "update Usuario set Nome = @Nome, Email = @Email, Senha = @Senha "
                      + "where Cod_Usuario = @Cod_Usuario";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Excluir(Usuario entity)
        {
            var query = "delete from Usuario where Cod_Usuario = @Cod_Usuario";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Usuario> ConsultarTodos()
        {
            var query = "select * from Usuario";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Usuario>(query).ToList();
            }
        }
        public Usuario ObterPorId(int id)
        {
            var query = "select * from Usuario where Cod_Usuario = @Cod_Usuario";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Usuario>
                (query, new { Cod_Usuario = id }).FirstOrDefault();
            }
        }

        public Usuario Obter(string email)
        {
            var query = "select * from Usuario where Email = @Email";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Usuario>
                (query, new { Email = email }).FirstOrDefault();
            }
        }

        public Usuario Obter(string email, string senha)
        {
            var query = "select * from Usuario where Email = @Email and Senha = @Senha";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Usuario>
                    (query, new
                    {
                        Email = email,
                        Senha = senha
                    }).FirstOrDefault();
            }
        }
    }
}
