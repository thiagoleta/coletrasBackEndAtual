using Projeto.Data.Seedwork.Notifying;
using Projeto.Data.Validators.PrimitiveValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Commands
{
    public class AtualizarMesReferenciaCommand : Validatable, IValidatable
    {

        public int Cod_MesReferencia { get; set; }
        public string MesAno { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataTermino { get; set; }
        public bool? Ativo { get; set; }


        public override void Validate()
        {
            if (!MesAno.HasMaxLength(8))
            {
                AddNotification(nameof(MesAno), "O Nome permite no máximo 8 caracteres");
            }
        }
    }
}
