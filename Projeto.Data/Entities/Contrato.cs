using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Projeto.Data.Entities
{
   public class Contrato
    {
		[Key]

		#region PrimareKey
		public int Cod_Contrato { get; set; }
		#endregion


		#region KeyColumns
		public int Coleta_Contratada { get; set; }
		public Decimal Valor_Limite { get; set; }
		public Decimal Valor_Unidade { get; set; }
		public bool Segunda { get; set; }
		public bool Terca { get; set; }
		public bool Quarta { get; set; }
		public bool Quinta { get; set; }
		public bool Sexta { get; set; }
		public bool Sabado { get; set; }
		public bool Domingo { get; set; }
		public bool Flag_Ativo { get; set; }
		public bool Flag_Cancelado { get; set; }
		public string Motivo_Cancelamento { get; set; }
		public DateTime Data_Cancelamento { get; set; }
		public bool Flag_Termino { get; set; }
		public DateTime Data_Inicio { get; set; }
		public DateTime Data_Termino { get; set; }
		public int Contrato_Numero { get; set; }
		#endregion

		#region Associacao ForeignKey
		//public List<Pagamento> Pagamento { get; set; }
		//public List<OS> OS { get; set; }
		#endregion

		#region Associacao RefForeignKey
		[ForeignKey("Cod_Cliente")]
		public int Cod_Cliente { get; set; }
		public Cliente Cliente { get; set; }
		[ForeignKey("Cod_Material")]
		public int Cod_Material { get; set; }
		public Material Material { get; set; }
		#endregion

	}
}
