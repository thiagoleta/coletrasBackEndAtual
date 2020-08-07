using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Entities
{
   public class Roteiro
    {
        public int Cod_Roteiro { get; set; }
        public int Cod_Cliente { get; set; }
        public int Cod_Turno { get; set; }
        public int Cod_Dia { get; set; }
        public int Cod_Rota { get; set; }
        public int Cod_Motorista { get; set; }
        public int Cod_Material { get; set; }

        public Cliente Cliente { get; set; }
        public Turno Turno { get; set; }
        public Dias_Coleta Dias_Coleta { get; set; }
        public Rota Rota { get; set; }
        public Motorista Motorista { get; set; }
        public Material Material { get; set; }


    }
}
