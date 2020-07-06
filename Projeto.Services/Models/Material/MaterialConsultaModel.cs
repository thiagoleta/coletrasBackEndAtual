using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Models.Material
{
    public class MaterialConsultaModel
    {
        //region PrimareKey
        public int Cod_Material { get; set; }

        //region KeyColumns
        public string Descricao { get; set; }
        public int Volume { get; set; }
        public string Observacao { get; set; }
        public string Material_Coletado { get; set; }

        //region Associacao RefForeignKey
    }
}
