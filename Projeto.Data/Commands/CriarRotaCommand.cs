using Projeto.Data.Seedwork.Notifying;
using Projeto.Data.Validators.PrimitiveValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Commands
{
    public class CriarRotaCommand : Validatable, IValidatable
    {
        
        public string Nome { get; set; }
        public string Composicao_Rota { get; set; }
        public bool Flag_Ativo { get; set; }
        public string Observacao { get; set; }

        public override void Validate()
        {
            if (!Nome.HasMaxLength(100))
            {
                AddNotification(nameof(Nome), "O Nome permite no máximo 100 caracteres");
            }

            if (!string.IsNullOrEmpty(Composicao_Rota) && Composicao_Rota.Length > 300)
            {
                AddNotification(nameof(Composicao_Rota), "O Campo descrição pode conter no máximo 300 caracteres.");
            }
            if (!string.IsNullOrEmpty(Observacao) && Observacao.Length > 800)
            {
                AddNotification(nameof(Observacao), "O Campo descrição pode conter no máximo 800 caracteres.");
            }
        }
    }
}
