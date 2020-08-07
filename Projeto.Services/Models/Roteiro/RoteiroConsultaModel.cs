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
    public class RoteiroConsultaModel
    {
        public int Cod_Roteiro { get; set; }
        public int Cod_Cliente { get; set; }
        public int Cod_Turno { get; set; }
        public int Cod_Dia { get; set; }
        public int Cod_Rota { get; set; }
        public int Cod_Motorista { get; set; }
        public int Cod_Material { get; set; }

        public ClienteConsultaModel Cliente { get; set; }
        public TurnoConsultaModel Turno { get; set; }
        public DiaColetaConsultaModel Dias_Coleta { get; set; }
        public RotaConsultaModel Rota { get; set; }
        public MotoristaConsultaModel Motorista { get; set; }
        public MaterialConsultaModel Material { get; set; }
    }
}
