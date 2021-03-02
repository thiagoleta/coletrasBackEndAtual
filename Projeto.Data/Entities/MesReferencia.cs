using Projeto.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Projeto.Data.Entities
{
   public class MesReferencia
    {
        private MesReferencia()
        {

        }

        public static MesReferencia Criar(DataString mesAno,
            DateTime dataInicio,
            DateTime? dataTermino,
            bool? ativo)            
        {
            var motorista = new MesReferencia()
            {

                MesAno = mesAno,
                DataInicio = dataInicio,
                DataTermino = dataTermino,
                Ativo = ativo   

            };            
            return motorista;
        }

        public void Atualizar(DataString mesAno,
                    DateTime dataInicio,
                    DateTime? dataTermino,
                    bool? ativo)
        {
            {
                MesAno = mesAno;
                DataInicio = dataInicio;
                DataTermino = dataTermino;
                Ativo = ativo;
            };
            //Validate();          
        }


        public int Cod_MesReferencia { get; set; }			
		public string MesAno { get; set; }		
		public DateTime DataInicio { get; set; }
		public DateTime? DataTermino { get; set; }
		public bool? Ativo { get; set; }		
			
	}
}
