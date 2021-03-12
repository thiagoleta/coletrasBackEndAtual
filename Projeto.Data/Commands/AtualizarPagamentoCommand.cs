using Projeto.Data.Seedwork.Notifying;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Commands
{
    public class AtualizarPagamentoCommand : Validatable, IValidatable
    {
        public int Cod_Pagamento { get; set; }
        public Decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public int Cod_MesReferencia { get; set; }
        public int Cod_Cliente { get; set; }

        public override void Validate()
        {
            if (Cod_MesReferencia <= 0)
            {
                AddNotification(nameof(Cod_MesReferencia), "O campo Mes Referência é obrigatório.");
            }
            if (Cod_Cliente <= 0)
            {
                AddNotification(nameof(Cod_Cliente), "O campo Cliente é obrigatório.");
            }
            if (Valor <= 0)
            {
                AddNotification(nameof(Valor), "O campo Valor é obrigatório.");
            }
            if (Data.Equals(null) || Data.Equals(""))
            {
                AddNotification(nameof(Data), "O Data Pagamento é obrigatória.");
            }
        }
    }
}
