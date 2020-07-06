using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Projeto.Data.Entities
{
   public class Cliente
    {
		[Key]

		#region PrimareKey
		public int Cod_Cliente { get; set; }
		#endregion


		#region KeyColumns
		public string CPF_CNPJ { get; set; }
		public string NomeCompleto_RazaoSocial { get; set; }
		public string Fantasia { get; set; }
		public string Insc_Estadual { get; set; }
		public string Logradouro { get; set; }
		public string Endereco { get; set; }
		public string Bairro { get; set; }
		public string Complemento { get; set; }
		public string Cidade { get; set; }
		public string CEP { get; set; }
		public string UF { get; set; }
		public string Telefones { get; set; }
		public string Funcao { get; set; }
		public string Email { get; set; }
		public bool Flag_Ativo { get; set; }
		public string Observacao { get; set; }
		public string Referencia { get; set; }
		public int OS_Numero { get; set; }
		public int Contrato_Numero { get; set; }
		public string Turno { get; set; }
		#endregion

		#region Associacao ForeignKey
		//public List<Contrato> Contrato { get; set; }
		#endregion

		#region Associacao RefForeignKey
		[ForeignKey("Cod_Rota")]
		public int Cod_Rota { get; set; }
		public Rota Rota { get; set; }
		#endregion


	}
}
