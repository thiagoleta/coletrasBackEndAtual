using Projeto.Data.Seedwork.Notifying;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Commands
{
   public class AtualizarRoteiroCommand : Validatable, IValidatable
    {
        public int Cod_Roteiro { get; set; }
        public int Cod_Cliente { get; set; }
        public int? Cod_Turno { get; set; }
        public int Cod_Rota { get; set; }
        public int Cod_Motorista { get; set; }
        public int? Cod_Material { get; set; }

        public bool? Segunda { get; set; }
        public bool? Terca { get; set; }
        public bool? Quarta { get; set; }
        public bool? Quinta { get; set; }
        public bool? Sexta { get; set; }
        public bool? Sabado { get; set; }
        public bool? Domingo { get; set; }
        public string Observacao { get; set; }

        public override void Validate()
        {
            if (Cod_Cliente <= 0)
            {
                AddNotification(nameof(Cod_Cliente), "O campo Cliente é obrigatório.");
            }
            if (Cod_Turno <= 0)
            {
                AddNotification(nameof(Cod_Turno), "O campo Turno é obrigatório.");
            }
            if (Cod_Rota <= 0)
            {
                AddNotification(nameof(Cod_Rota), "O campo Rota é obrigatório.");
            }
            if (Cod_Motorista <= 0)
            {
                AddNotification(nameof(Cod_Motorista), "O campo Motorista é obrigatório.");
            }
        }
    }
}
