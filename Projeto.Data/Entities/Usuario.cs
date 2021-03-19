using Projeto.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Projeto.Data.Entities
{
   public class Usuario
    {
        public Usuario()
        {

        }

        public Usuario(int cod_Usuario, string nome)
        {
            this.Cod_Usuario = cod_Usuario;
            this.Nome = nome;
        }

        public static Usuario Criar(DataString nome,
                                      string email,
                                      string senha,
                                      Perfil perfil) {

            var usuario = new Usuario
            {
                Nome = nome,
                Email = email,
                Senha = senha,
                Cod_Perfil = perfil.Cod_Perfil,
                DataCriacao = DateTime.Now,
        };
        
        return usuario;
    }

        public void Atualizar(DataString nome,
                                      string email,
                                      string senha,
                                      Perfil perfil)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Cod_Perfil = perfil.Cod_Perfil;                
        }

        public int Cod_Usuario { get; set; }
        public int Cod_Perfil { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataCriacao { get; set; }
        public Perfil Perfil { get; set; }
    }
}
