using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Projeto.Data.Entities
{
   public class Pagamento
    {

        public Pagamento()
        {

        }

        public static Pagamento Criar(decimal valor,
                 DateTime data,
                 MesReferencia mesReferencia,
                 Cliente cliente)
        {
            var pagamento = new Pagamento()
            {

                Valor = valor,
                Data = data,
                Cod_Cliente = cliente.Cod_Cliente,
                Cod_MesReferencia = mesReferencia.Cod_MesReferencia,

            };
            
            return pagamento;
        }


        public void Atualizar(decimal valor,
           DateTime data,
           MesReferencia mesReferencia,
           Cliente cliente)
        {
            {

                Valor = valor;
                Data = data;
                Cod_Cliente = cliente.Cod_Cliente;
                Cod_MesReferencia = mesReferencia.Cod_MesReferencia;                

            };
            
        }

        public int Cod_Pagamento { get; set; }		
		public Decimal Valor { get; set; }
		public DateTime Data { get; set; }	
		
		public int Cod_MesReferencia { get; set; }

        public int Cod_Cliente { get; set; }
        public MesReferencia MesReferencia { get; set; }		

        public Cliente Cliente { get; set; }        

    }
}
