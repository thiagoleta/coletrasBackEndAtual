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

        public Perfil(int cod_Perfil, string nome_Perfil)
        {
            this.Cod_Perfil = cod_Perfil;
            this.Nome_Perfil = nome_Perfil;
        }

        public static Perfil Criar(DataString nome_Perfil)
        
        {
            var perfil = new Perfil
            {
                Nome_Perfil = nome_Perfil,                
            };

            return perfil;
        }

        public void Atualizar(DataString nome_Perfil) 
        {
            Nome_Perfil = nome_Perfil;            
        }

        public int Cod_Perfil { get; set; }
        public string Nome_Perfil { get; set; }
        
    }
}
