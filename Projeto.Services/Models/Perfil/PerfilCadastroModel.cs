﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Models.Perfil
{
    public class PerfilCadastroModel
    {
        [Required(ErrorMessage = "Informe o Nome Perfil.", AllowEmptyStrings = false)]
        public string Nome_Perfil { get; set; }
        public int Cod_Usuario { get; set; }
    }
}
