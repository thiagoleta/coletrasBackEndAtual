using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Projeto.Data.Entities
{
   public class Rota
    {
        [Key]
        public int CodRota { get; set; }
        [ForeignKey("Cod_Motorista")]
        public int Cod_Motorista { get; set; }
        public string Nome { get; set; }
        public string Composicao_Rota { get; set; }
        public bool Flag_Ativo { get; set; }
        public string Observacao { get; set; }

        #region Associações
        public Motorista Motorista { get; set; }
        #endregion


    }
}
