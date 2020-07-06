using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Projeto.Data.Entities
{
   public class Material
    {
		[Key]

		#region PrimareKey
		public int Cod_Material { get; set; }
		#endregion


		#region KeyColumns
		public string Descricao { get; set; }
		public int Volume { get; set; }
		public string Observacao { get; set; }
		public string Material_Coletado { get; set; }
		#endregion

		#region Associacao ForeignKey
		//public List<Contrato> Contrato { get; set; }
		#endregion

		#region Associacao RefForeignKey
		#endregion

	}
}
