using Projeto.Services.Models.Cliente;
using Projeto.Services.Models.DiasSemana;
using Projeto.Services.Models.Material;
using Projeto.Services.Models.Motorista;
using Projeto.Services.Models.Rota;
using Projeto.Services.Models.Turno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Models.Roteiro
{
    public class RoteiroEdicaoModel
    {
        public int Cod_Roteiro { get; set; }
        public int Cod_Cliente { get; set; }
        public int Cod_Turno { get; set; }
        public int Cod_Dia { get; set; }
        public int Cod_Rota { get; set; }
        public int Cod_Motorista { get; set; }
        public int? Cod_Material { get; set; }

    }
}
