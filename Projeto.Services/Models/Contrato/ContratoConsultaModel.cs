using Projeto.Services.Models.Cliente;
using Projeto.Services.Models.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Models.Contrato
{
    public class ContratoConsultaModel
    {
        
        public int Cod_Contrato { get; set; }

        
        public int Coleta_Contratada { get; set; }
        public Decimal Valor_Limite { get; set; }
        public Decimal Valor_Unidade { get; set; }
        
        public bool Flag_Ativo { get; set; }
        public bool Flag_Cancelado { get; set; }
        public string Motivo_Cancelamento { get; set; }
        public DateTime Data_Cancelamento { get; set; }
        public bool Flag_Termino { get; set; }
        public DateTime Data_Inicio { get; set; }
        public DateTime Data_Termino { get; set; }        

        public MaterialConsultaModel Material { get; set; }
        public ClienteConsultaModel Cliente { get; set; }
    }
}
