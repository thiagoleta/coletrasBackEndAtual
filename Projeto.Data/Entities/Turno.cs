using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Entities
{
   public class Turno
    {

        public Turno()
        {

        }

        public Turno(int cod_turno, string nomeTurno)
        {
            this.Cod_Turno = cod_turno;
            this.Nome_Turno = nomeTurno;
        }

        public int Cod_Turno { get; set; }
        public string Nome_Turno { get; set; }
    }
}
