using Projeto.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Projeto.Data.Entities
{
   public class Perfil
    {
        public Perfil()
        {

        }

        public static Perfil Criar(DataString nome_Perfil,
                            Usuario usuario)
        
        {
            var perfil = new Perfil
            {
                Nome_Perfil = nome_Perfil,
                Cod_Perfil = usuario.Cod_Usuario,
            };

            return perfil;
        }

        public void Atualizar(DataString nome_Perfil,
                                 Usuario usuario) 
        {
            Nome_Perfil = nome_Perfil;
            Cod_Usuario = usuario.Cod_Usuario;
        }

        public int Cod_Perfil { get; set; }
        public string Nome_Perfil { get; set; }
        public int Cod_Usuario { get; set; }

        public Usuario Usuario { get; set; }

        
    }
}
