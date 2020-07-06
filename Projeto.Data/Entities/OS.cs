using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Projeto.Data.Entities
{
   public class OS
    {
		[Key]

		#region PrimareKey
		public int Cod_OS { get; set; }
		#endregion


		#region KeyColumns
		public DateTime Data_Geracao { get; set; }
		public int Quantidade_Coletada { get; set; }
		public DateTime Data_Coleta { get; set; }
		public bool Flag_Coleta { get; set; }
		public bool Flag_Ativo { get; set; }
		public int OS_Numero { get; set; }
		public bool Flag_Cancelado { get; set; }
		public string Motivo_Cancelamento { get; set; }
		public DateTime Data_Cancelamento { get; set; }
		#endregion

		#region Associacao ForeignKey
		#endregion

		#region Associacao RefForeignKey
		[ForeignKey("Cod_MesReferencia")]
		public int Cod_MesReferencia { get; set; }
		public MesReferencia MesReferencia { get; set; }
		[ForeignKey("Cod_Contrato")]
		public int Cod_Contrato { get; set; }
		public Contrato Contrato { get; set; }
		#endregion

	}
}
