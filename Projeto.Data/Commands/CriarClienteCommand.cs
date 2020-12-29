using Projeto.Data.Seedwork.Notifying;
using Projeto.Data.Validators.PrimitiveValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Commands
{
   public class CriarClienteCommand : Validatable, IValidatable
    {
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
        public bool? Flag_Ativo { get; set; }
        public string Observacao { get; set; }
        public string Referencia { get; set; }

        public override void Validate()
        {
            if (!CPF_CNPJ.HasMaxLength(20))
            {
                AddNotification(nameof(CPF_CNPJ), "O Nome permite no máximo 20 caracteres");
            }
            if (!NomeCompleto_RazaoSocial.HasMaxLength(250))
            {
                AddNotification(nameof(NomeCompleto_RazaoSocial), "O Nome permite no máximo 250 caracteres");
            }
            if (!Fantasia.HasMaxLength(250))
            {
                AddNotification(nameof(Fantasia), "O Nome permite no máximo 250 caracteres");
            }
            if (!Email.HasMaxLength(200))
            {
                AddNotification(nameof(Email), "O Nome permite no máximo 200 caracteres");
            }
        }
    }
}
