using Projeto.Data.Seedwork.Notifying;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Commands
{
    public class AtualizarOsCommand : Validatable, IValidatable
    {
		public int Cod_OS { get; set; }
		public DateTime Data_Geracao { get; set; }
		public int Quantidade_Coletada { get; set; }
		public DateTime? Data_Coleta { get; set; }
		public bool? Flag_Coleta { get; set; }
		public bool? Flag_Envio_Email { get; set; }
		public bool? Flag_Cancelado { get; set; }
		public string Motivo_Cancelamento { get; set; }
		public DateTime? Data_Cancelamento { get; set; }
		public string Hora_Entrada { get; set; }
		public string Hora_Saida { get; set; }
		
		
		public int Cod_Cliente { get; set; }

		public int Cod_MesReferencia { get; set; }
		

		public int Cod_Material { get; set; }
		

		public int Cod_Motorista { get; set; }

		public int Cod_Frota { get; set; }

		public override void Validate()
        {
			if (Cod_MesReferencia <= 0)
			{
				AddNotification(nameof(Cod_MesReferencia), "O campo Mes Referência é obrigatório.");
			}
			if (Cod_Cliente <= 0)
			{
				AddNotification(nameof(Cod_Cliente), "O campo Cliente é obrigatório.");
			}
			if (Cod_Material <= 0)
			{
				AddNotification(nameof(Cod_Cliente), "O campo Cliente é obrigatório.");
			}
			if (Cod_Motorista <= 0)
			{
				AddNotification(nameof(Cod_Cliente), "O campo Cliente é obrigatório.");
			}
			if (Cod_Frota <= 0)
			{
				AddNotification(nameof(Cod_Cliente), "O campo Placa é obrigatório.");
			}
			if (Data_Coleta.Equals(null))
			{
				AddNotification(nameof(Data_Coleta), "Informe a data que foi feita a coleta.");
			}
			if (Quantidade_Coletada < 0)
			{
				AddNotification(nameof(Quantidade_Coletada), "Informe Quantidade de sacos coletados.");
			}
			if (!string.IsNullOrEmpty(Hora_Entrada) && Hora_Entrada.Length > 10)
			{
				AddNotification(nameof(Hora_Entrada), "O Campo Hora Entrada pode conter no máximo 10 caracteres.");
			}
			if (!string.IsNullOrEmpty(Hora_Saida) && Hora_Saida.Length > 10)
			{
				AddNotification(nameof(Hora_Saida), "O Campo Hora Saida pode conter no máximo 10 caracteres.");
			}
			if (!string.IsNullOrEmpty(Motivo_Cancelamento) && Motivo_Cancelamento.Length > 10)
			{
				AddNotification(nameof(Motivo_Cancelamento), "O Campo Motivo Cancelamento pode conter no máximo 150 caracteres.");
			}
		
		}
    }
}
