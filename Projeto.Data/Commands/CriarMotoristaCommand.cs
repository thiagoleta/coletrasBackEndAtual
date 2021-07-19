using Projeto.Data.Seedwork.Notifying;
using Projeto.Data.Validators.PrimitiveValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Commands
{
    public class CriarMotoristaCommand : Validatable, IValidatable
    {
        public string Nome { get; set; }
        public string Ajudante1 { get; set; }
        public string Observacao { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
       

        public override void Validate()
        {
            if (!Nome.HasMaxLength(200))
            {
                AddNotification(nameof(Nome), "O Nome permite no máximo 200 caracteres");
            }         
        }
    }
}

