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

		[Required(ErrorMessage = "Informe o nome do Motorista.", AllowEmptyStrings = false)]
		public string Nome { get; set; }
		
		public string Ajudante1 { get; set; }
		
		public string Ajudante2 { get; set; }
		[Required(ErrorMessage = "Informe a placa.", AllowEmptyStrings = false)]
		public string Placa { get; set; }		
		public string Telefone1 { get; set; }		
		public string Telefone2 { get; set; }


	}
}
