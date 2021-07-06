using Projeto.Data.Seedwork.Notifying;
using Projeto.Data.Validators.PrimitiveValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Commands
{
    public class CriarFrotaCommand : Validatable, IValidatable
    {
        
        public int Cod_Motorista { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public string Placa { get; set; }
        public string KM { get; set; }

        public override void Validate()
        {
            if (!Descricao.HasMaxLength(200))
            {
                AddNotification(nameof(Descricao), "O Descricao permite no máximo 200 caracteres");
            }
            if (!Observacao.HasMaxLength(200))
            {
                AddNotification(nameof(Observacao), "O Observação permite no máximo 400 caracteres");
            }
            if (!Placa.HasMaxLength(20))
            {
                AddNotification(nameof(Placa), "O Nome permite no máximo 20 caracteres");
            }
        }
    }
}
