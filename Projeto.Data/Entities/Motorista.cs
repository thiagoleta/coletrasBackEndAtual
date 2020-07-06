using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Projeto.Data.Entities
{
   public class Motorista
    {
		[Key]
		public int Cod_Motorista { get; set; }
		public string Nome { get; set; }
		public string Ajudante1 { get; set; }
		public string Ajudante2 { get; set; }
		public string Placa { get; set; }
		public string Telefone1 { get; set; }
		public string Telefone2 { get; set; }

		#region Associação
		//public Rota Rota { get; set; }


		#endregion


	}
}
