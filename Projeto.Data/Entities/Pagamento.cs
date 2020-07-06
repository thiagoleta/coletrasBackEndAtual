using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Projeto.Data.Entities
{
   public class Pagamento
    {
		[Key]

		#region PrimareKey
		public int Cod_Pagamento { get; set; }
		#endregion


		#region KeyColumns
		public Decimal Valor { get; set; }

		public DateTime Data { get; set; }
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
