using Projeto.Data.Seedwork.Notifying;
using Projeto.Data.Validators.PrimitiveValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Commands
{
    public class AtualizarUsuarioCommand : Validatable, IValidatable
    {
        public int Cod_Usuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int Cod_Perfil { get; set; }

        public override void Validate()
        {
            if (!Nome.HasMaxLength(100))
            {
                AddNotification(nameof(Nome), "O Nome permite no máximo 100 caracteres");
            }
            if (!Email.HasMaxLength(100))
            {
                AddNotification(nameof(Email), "O Email permite no máximo 100 caracteres");
            }            
        }
    }
}
