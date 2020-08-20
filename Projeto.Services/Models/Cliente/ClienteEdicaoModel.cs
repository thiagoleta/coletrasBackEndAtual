using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Models.Cliente
{
    public class ClienteEdicaoModel
    {        
        public int Cod_Cliente { get; set; }
        public string CPF_CNPJ { get; set; }
        [Required(ErrorMessage = "Informe o NomeCompleto/RazaoSocial do Cliente.", AllowEmptyStrings = false)]
        public string NomeCompleto_RazaoSocial { get; set; }
        [Required(ErrorMessage = "Informe o Fantasia do Cliente.", AllowEmptyStrings = false)]
        public string Fantasia { get; set; }
        public string Insc_Estadual { get; set; }
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "Informe o Endereço do Cliente.", AllowEmptyStrings = false)]
        public string Endereco { get; set; }
        [Required(ErrorMessage = "Informe o Bairro do Cliente.", AllowEmptyStrings = false)]
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string CEP { get; set; }
        public string UF { get; set; }
        [Required(ErrorMessage = "Informe os Telefones do Cliente.", AllowEmptyStrings = false)]
        public string Telefones { get; set; }
        public string Funcao { get; set; }
        [Required(ErrorMessage = "Informe o Email do Cliente.", AllowEmptyStrings = false)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe se CLiente está Ativo.", AllowEmptyStrings = false)]
        public bool Flag_Ativo { get; set; }
        public string Observacao { get; set; }
        public string Referencia { get; set; }


    }
}
