using Projeto.Data.Seedwork.Notifying;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Commands
{
    public class CriarContratoCommand : Validatable, IValidatable

	{

		public int ColetaContratada { get; set; }
		public Decimal? ValorLimite { get; set; }
		public Decimal ValorUnidade { get; set; }

		public string MotivoCancelamento { get; set; }
		public DateTime? DataCancelamento { get; set; }
		public bool? FlagTermino { get; set; }
		public DateTime DataInicio { get; set; }
		public DateTime? DataTermino { get; set; }
		public int Cod_Cliente { get; set; }


		public override void Validate()
		{
			if (ValorUnidade <= 0)
			{
				AddNotification(nameof(ValorUnidade), "O Valor Unidade é obrigatório");
			}
			if (DataInicio.Equals(null) || DataInicio.Equals(""))
			{
				AddNotification(nameof(DataInicio), "O Data Inicio  é obrigatória.");
			}
		}
	}
}