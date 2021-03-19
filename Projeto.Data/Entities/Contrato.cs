using Projeto.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Projeto.Data.Entities
{
	public class Contrato
    {

    

		public static Contrato Criar(int coletaContratada,
										decimal? valorLimite,
										decimal valorUnidade,
										DataString? motivoCancelamento,
										DateTime? dataCancelamento,
										string flagTermino,
										DateTime dataInicio,
										DateTime? dataTermino,
										Cliente cliente)



		{
			var contrato = new Contrato()
			{
				ColetaContratada = coletaContratada,
				ValorLimite = valorLimite,
				ValorUnidade = valorUnidade,
				MotivoCancelamento = motivoCancelamento,
				DataCancelamento = dataCancelamento,
				FlagTermino = flagTermino,
				DataInicio = dataInicio,
				DataTermino = dataTermino,
				Cod_Cliente = cliente.Cod_Cliente,
				
			};
			return contrato;
		}

		//public void Atualizar(int coletaContratada,
		//						decimal? valorLimite,
		//						decimal valorUnidade,
		//						DataString? motivoCancelamento,
		//						DateTime? dataCancelamento,
		//						DataString flagTermino,
		//						DateTime dataInicio,
		//						DateTime? dataTermino,
		//						Cliente cliente)

		//{
		//	ColetaContratada = coletaContratada;
		//	ValorLimite = valorLimite;
		//	ValorUnidade = valorUnidade;
		//	MotivoCancelamento = motivoCancelamento;
		//	DataCancelamento = dataCancelamento;
		//	FlagTermino = flagTermino;
		//	DataInicio = dataInicio;
		//	DataTermino = dataTermino;
		//	Cod_Cliente = cliente.Cod_Cliente;
		//}


		public void Atualizar(int coletaContratada,
								decimal? valorLimite,
								decimal valorUnidade,
								DataString? motivoCancelamento,
								DateTime? dataCancelamento,
								DataString flagTermino,
								DateTime dataInicio,
								DateTime? dataTermino,
								Cliente cliente)
		{
			ColetaContratada = coletaContratada;
			ValorLimite = valorLimite;
			ValorUnidade = valorUnidade;
			MotivoCancelamento = motivoCancelamento;
			DataCancelamento = dataCancelamento;
			FlagTermino = flagTermino;
			DataInicio = dataInicio;
			DataTermino = dataTermino;
			Cod_Cliente = cliente.Cod_Cliente;

			
		}



		public int Cod_Contrato { get; set; }

		public int ColetaContratada { get; set; }
		public Decimal? ValorLimite { get;  set; }
		public Decimal ValorUnidade { get;  set; }

		public string MotivoCancelamento { get;  set; }
		public DateTime? DataCancelamento { get;  set; }
		public string FlagTermino { get;  set; }
		public DateTime DataInicio { get;  set; }
		public DateTime? DataTermino { get;  set; }
		public int Cod_Cliente { get;  set; }

		public Cliente Cliente { get;  set; }



	}
}
