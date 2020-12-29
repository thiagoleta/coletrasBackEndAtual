using Projeto.Data.Seedwork.Notifying;
using Projeto.Data.Validators.PrimitiveValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Commands
{
    public class CriarMaterialCommand : Validatable, IValidatable
    {        
        public string Descricao { get; set; }
        public string Volume { get; set; }
        public string Observacao { get; set; }
        public string Material_Coletado { get; set; }

        public override void Validate()
        {
            if (!Descricao.HasMaxLength(200))
            {
                AddNotification(nameof(Descricao), "O Descricao permite no máximo 200 caracteres");
            }
        }
    }
}
