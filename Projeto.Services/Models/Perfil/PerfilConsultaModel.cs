using Projeto.Services.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Models.Perfil
{
    public class PerfilConsultaModel
    {
        public int Cod_Perfil { get; set; }
        public string Nome_Perfil { get; set; }        

        public UsuarioConsultaModel Usuario { get; set; }

    }
}
