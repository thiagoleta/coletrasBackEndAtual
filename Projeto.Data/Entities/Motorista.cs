using Projeto.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Projeto.Data.Entities
{
   public class Motorista
    {
		private Motorista()
		{ 

		}

        public static Motorista Criar(DataString nome,
            DataString? ajudante1,
            DataString? ajudante2,
            DataString placa, 
            DataString? telefone1,
            DataString? telefone2)
        {
            var motorista = new Motorista()
            {
                
                Nome = nome,
                Ajudante1 = ajudante1,
                Ajudante2 = ajudante2,
                Placa = placa,                
                Telefone1 = telefone1,
                Telefone2 = telefone2,


            };
            //motorista.Validate();
            return motorista;
        }

        public void Atualizar(DataString nome,
            DataString? ajudante1,
            DataString? ajudante2, 
            DataString placa,
            DataString? telefone1,
            DataString? telefone2)
        {            
            {

                Nome = nome;
                Ajudante1 = ajudante1;
                Ajudante2 = ajudante2;
                Placa = placa;
                Telefone1 = telefone1;
                Telefone2 = telefone2;

            };
            //Validate();
        }

        public int Cod_Motorista { get; set; }
		public string Nome { get; set; }
		public string Ajudante1 { get; set; }
		public string Ajudante2 { get; set; }
		public string Placa { get; set; }
		public string Telefone1 { get; set; }
		public string Telefone2 { get; set; }
	}

}
