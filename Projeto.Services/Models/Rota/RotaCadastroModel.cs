﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Models.Rota
{
    public class RotaCadastroModel
    {        
        
        public string Nome { get; set; }
        public string Composicao_Rota { get; set; }
        public bool Flag_Ativo { get; set; }
        public string Observacao { get; set; }

    }
}
