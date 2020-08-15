using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Projeto.Data.Entities
{
   public class MesReferencia
    {	
		
		public int Cod_MesReferencia { get; set; }	
		
		public string Codigo { get; set; }
		public int Mes { get; set; }
		public int Ano { get; set; }
		public DateTime Data_Inicio { get; set; }
		public DateTime? Data_Termino { get; set; }
		public bool Flag_Encerramento { get; set; }
		public DateTime? Data_Encerramento { get; set; }	
			
	}
}
