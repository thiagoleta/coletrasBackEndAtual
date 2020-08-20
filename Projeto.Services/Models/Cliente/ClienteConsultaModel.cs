using Projeto.Services.Models.Rota;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Models.Cliente
{
    public class ClienteConsultaModel
    {
        public int Cod_Cliente { get; set; }
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

    }
}
