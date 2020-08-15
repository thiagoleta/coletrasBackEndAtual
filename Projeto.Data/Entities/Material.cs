using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Projeto.Data.Entities
{
   public class Material
    {	
		public int Cod_Material { get; set; }
		public string Descricao { get; set; }
		public int Volume { get; set; }
		public string Observacao { get; set; }
		public string Material_Coletado { get; set; }		

	}
}
