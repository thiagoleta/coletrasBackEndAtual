using Projeto.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Projeto.Data.Entities
{
   public class OS
    {

        public OS()
        {

        }

		public static OS Criar(Cliente cliente,
								MesReferencia mesRef)
		{
			var os = new OS()
			{
				Cod_Cliente = cliente.Cod_Cliente,
				Cod_MesReferencia = mesRef.Cod_MesReferencia,
				Data_Geracao = DateTime.Now,
				Flag_Coleta = false,
				Flag_Envio_Email = true,
				Flag_Cancelado = false				
			};
			return os;
		}

		public void Atualizar(Cliente cliente,
								MesReferencia mesRef,
								Material material,
								Motorista motorista,
								Frota frota,
								 int quantidade_Coletada,
								 DateTime? data_Coleta,
								 bool? flag_Coleta,
								 bool? flag_Envio_Email,
								 bool? flag_Cancelado,								 
								 DataString? motivo_Cancelamento,
								 DateTime? data_Cancelamento,
								 DataString? hora_Entrada,
								 DataString? hora_Saida) 
		{
			{
				Cod_Cliente = cliente.Cod_Cliente;
				Cod_MesReferencia = mesRef.Cod_MesReferencia;
				Cod_Material = material.Cod_Material;
				Cod_Motorista = motorista.Cod_Motorista;
				Cod_Frota = frota.Cod_Frota;
				Quantidade_Coletada = quantidade_Coletada;
				Data_Coleta = data_Coleta;
				Flag_Coleta = flag_Coleta;
				Flag_Envio_Email = flag_Envio_Email;
				Flag_Cancelado = flag_Cancelado;
				Motivo_Cancelamento = motivo_Cancelamento;
				Data_Cancelamento = data_Cancelamento;
				Hora_Entrada = hora_Entrada;
				Hora_Saida = hora_Saida;				
			};
			
		}


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


		public List<Cliente> Clientes { get; set; }
		public Cliente Cliente { get; set; }
		public int Cod_Cliente { get; set; }		

		public int? Cod_MesReferencia { get; set; }
		public MesReferencia MesReferencia { get; set; }

		public int? Cod_Material { get; set; }
		public Material Material { get; set; }

		public int? Cod_Motorista { get; set; }
		public Motorista Motorista { get; set; }

		public int Cod_Frota { get; set; }
		public Frota Frota { get; set; }
		//private int AtualizarCllienteOs(IEnumerable<Cliente> clientes)
		//{

		//	foreach (var cliente in clientes)
		//	{
		//		int cod_Cliente;
		//		var os = new OS();

		//		os.Cod_Cliente = cliente.Cod_Cliente;

		//		cod_Cliente = os.Cod_Cliente;

		//	}
		//	return cod_Cliente;
		//}



	}
}
