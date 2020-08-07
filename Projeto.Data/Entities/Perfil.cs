using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Projeto.Data.Entities
{
   public class Perfil
    {
        public int Cod_Perfil { get; set; }
        public string Nome_Perfil { get; set; }

        public int Cod_Usuario { get; set; }


        public Usuario Usuario { get; set; }
    }
}
