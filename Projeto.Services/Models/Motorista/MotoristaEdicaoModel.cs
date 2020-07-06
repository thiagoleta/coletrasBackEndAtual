using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Models.Motorista
{
    public class MotoristaEdicaoModel
    {
		[Required(ErrorMessage = "Informe o Código do Motorista.")]
		public int CodMotorista { get; set; }


		[Required(ErrorMessage = "Informe o nome do Motorista.")]
		public string Nome { get; set; }
		[Required(ErrorMessage = "Informe o nome do Ajudante1.")]
		public string Ajudante1 { get; set; }
		[Required(ErrorMessage = "Informe o nome do Ajudante2.")]
		public string Ajudante2 { get; set; }
		[Required(ErrorMessage = "Informe a placa.")]
		public string Placa { get; set; }

		[Required(ErrorMessage = "Informe o Telefone1.")]
		public string Telefone1 { get; set; }
		[Required(ErrorMessage = "Informe o Telefone2.")]
		public string Telefone2 { get; set; }


	}
}
