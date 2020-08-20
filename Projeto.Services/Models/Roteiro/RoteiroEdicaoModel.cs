using Projeto.Services.Models.Cliente;
using Projeto.Services.Models.DiasSemana;
using Projeto.Services.Models.Material;
using Projeto.Services.Models.Motorista;
using Projeto.Services.Models.Rota;
using Projeto.Services.Models.Turno;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Models.Roteiro
{
    public class RoteiroEdicaoModel
    {
        public int Cod_Roteiro { get; set; }
        [Required(ErrorMessage = "Informe o Cliente para o Roteiro", AllowEmptyStrings = false)]
        public int Cod_Cliente { get; set; }
        [Required(ErrorMessage = "Informe o Turno para o Roteiro", AllowEmptyStrings = false)]
        public int Cod_Turno { get; set; }
        [Required(ErrorMessage = "Informe o Dia das Coletas para o Roteiro", AllowEmptyStrings = false)]
        public int Cod_Dia { get; set; }
        [Required(ErrorMessage = "Informe a _Rota para o Roteiro", AllowEmptyStrings = false)]
        public int Cod_Rota { get; set; }
        [Required(ErrorMessage = "Informe o Motorista para o Roteiro", AllowEmptyStrings = false)]
        public int Cod_Motorista { get; set; }
        [Required(ErrorMessage = "Informe o Material para o Roteiro", AllowEmptyStrings = false)]
        public int Cod_Material { get; set; }

    }
}
