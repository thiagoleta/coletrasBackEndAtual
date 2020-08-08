using Projeto.Services.Models.Cliente;
using Projeto.Services.Models.Contrato;
using Projeto.Services.Models.MesReferencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Models.OS
{
    public class OSCadastroModel
    {		
				
		public DateTime Data_Geracao { get; set; }
		public int Quantidade_Coletada { get; set; }
		public DateTime? Data_Coleta { get; set; }
		public bool? Flag_Coleta { get; set; }
		public bool? Flag_Ativo { get; set; }
		public bool? Flag_Cancelado { get; set; }
		public string Motivo_Cancelamento { get; set; }
		public DateTime? Data_Cancelamento { get; set; }	

		

		public int Cod_MesReferencia { get; set; }
		//public MesReferenciaConsultaModel MesReferencia { get; set; }

		public int Cod_Contrato { get; set; }
		//public ContratoConsultaModel Contrato { get; set; }

		public List<ClienteOSModel> Clientes { get; set; }
		//public int Cod_Cliente { get; set; }
		


	}
}
