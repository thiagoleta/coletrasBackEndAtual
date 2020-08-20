using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Models.Configucacao
{
    public class ConfiguracaoConsultaModel
    {
		public int Cod_Configuracao { get; set; }
		public string Empresa { get; set; }
		public string Numero_Inea { get; set; }
		public string Inscr_Municipal { get; set; }
		public string CNPJ { get; set; }
		public string Endereco { get; set; }
		public string Bairro { get; set; }
		public string Cidade { get; set; }
		public string Telefones { get; set; }
		
		public bool Flag_Ativo { get; set; }

	}
}
