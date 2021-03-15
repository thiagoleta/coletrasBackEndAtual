using Projeto.Data.Seedwork.Notifying;
using Projeto.Data.Validators.PrimitiveValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Commands
{
   public class AtualizarPerfilCommand : Validatable, IValidatable
    {
        public int Cod_Perfil { get; set; }
        public string Nome_Perfil { get; set; }
        public int Cod_Usuario { get; set; }
        public override void Validate()
        {
      
            if (!Nome_Perfil.HasMaxLength(250))
            {
                AddNotification(nameof(Nome_Perfil), "O Nome Perfil permite no máximo 250 caracteres");
            }
            if (Cod_Usuario <= 0)
            {
                AddNotification(nameof(Cod_Usuario), "O campo Cod_Usuario é obrigatório.");
            }
        }
    }
}
