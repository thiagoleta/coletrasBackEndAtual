using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Models.Material
{
    public class MaterialEdicaoModel
    {        
        public int Cod_Material { get; set; }
        [Required(ErrorMessage = "Informe a Descrição do Material.", AllowEmptyStrings = false)]
        public string Descricao { get; set; }
        public int Volume { get; set; }
        public string Observacao { get; set; }
        public string Material_Coletado { get; set; }        

    }
}
