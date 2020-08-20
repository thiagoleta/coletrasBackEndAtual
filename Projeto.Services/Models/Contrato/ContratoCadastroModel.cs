using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Models.Contrato
{
    public class ContratoCadastroModel
    {
        [Required(ErrorMessage = "Informe a Coleta Contratada do Contrato.", AllowEmptyStrings = false)]
        public int Coleta_Contratada { get; set; }
        [Required(ErrorMessage = "Informe a Valor Limite do Contrato.", AllowEmptyStrings = false)]
        public Decimal Valor_Limite { get; set; }
        [Required(ErrorMessage = "Informe a Valor Unidade do Contrato.", AllowEmptyStrings = false)]
        public Decimal Valor_Unidade { get; set; }
        [Required(ErrorMessage = "Informe se o contrato está Ativo do Contrato.", AllowEmptyStrings = false)]
        public bool Flag_Ativo { get; set; }
        public bool Flag_Cancelado { get; set; }
        public string Motivo_Cancelamento { get; set; }
        public DateTime? Data_Cancelamento { get; set; }
        public bool Flag_Termino { get; set; }
        [Required(ErrorMessage = "Informe a Data de Inicio do Contrato.", AllowEmptyStrings = false)] 
        public DateTime Data_Inicio { get; set; }
        public DateTime? Data_Termino { get; set; }
        [Required(ErrorMessage = "Informe o Nome do CLiente Associado ao Contrato.", AllowEmptyStrings = false)]
        public int Cod_Cliente { get; set; }
        

    }
}
