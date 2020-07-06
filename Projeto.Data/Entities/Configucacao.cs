using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Projeto.Data.Entities
{
   public class Configucacao
    {
		[Key]

		#region PrimareKey
		public int Cod_Configucacao { get; set; }
		#endregion


		#region KeyColumns
		public string Empresa { get; set; }
		public string Numero_Inea { get; set; }
		public string Logradouro { get; set; }
		public string Endereco { get; set; }
		public string Bairro { get; set; }
		public string Complemento { get; set; }
		public string Cidade { get; set; }
		public string CEP { get; set; }
		public string UF { get; set; }
		public string Telefones { get; set; }
		#endregion

		#region Associacao ForeignKey
		#endregion

		#region Associacao RefForeignKey
		#endregion

	}
}
