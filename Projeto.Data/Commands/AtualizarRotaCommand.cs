using Projeto.Data.Seedwork.Notifying;
using Projeto.Data.Validators.PrimitiveValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Commands
{
    public class AtualizarRotaCommand : Validatable, IValidatable
    {
        public int Cod_Rota { get; set; }
        public string Nome { get; set; }
        public string Composicao_Rota { get; set; }
        public bool? Flag_Ativo { get; set; }
        public string Observacao { get; set; }

        public override void Validate()
        {
            if (!Nome.HasMaxLength(100))
            {
                AddNotification(nameof(Nome), "O Nome permite no máximo 100 caracteres");
            }
        }
    }
}
