using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Models.OS
{
    public class OSEdicaoModel
    {        
        public int Cod_OS { get; set; }        
        //public DateTime Data_Geracao { get; set; }
        public int? Quantidade_Coletada { get; set; }
        public DateTime? Data_Coleta { get; set; }
        public bool Flag_Coleta { get; set; }
        //public bool Flag_Ativo { get; set; }        
        public bool Flag_Cancelado { get; set; }
        public string Motivo_Cancelamento { get; set; }
        public DateTime? Data_Cancelamento { get; set; }

        public int Cod_MesReferencia { get; set; }
        public int Cod_Contrato { get; set; }
        public int Cod_Cliente { get; set; }
        public int Cod_Configuracao { get; set; }

    }
}
