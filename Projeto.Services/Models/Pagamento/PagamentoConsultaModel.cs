using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Models.Pagamento
{
    public class PagamentoConsultaModel
    {
        //region PrimareKey
        public int Cod_Pagamento { get; set; }

        //region KeyColumns
        public Decimal Valor { get; set; }
        public DateTime Data { get; set; }

        //region Associacao RefForeignKey
        public int Cod_MesReferencia { get; set; }
        public int Cod_Contrato { get; set; }
    }
}
