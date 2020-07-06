using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Models.Usuario
{
    public class UsuarioLoginModel
    {
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, infome o email de acesso.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, infome a senha de acesso.")]
        public string Senha { get; set; }

    }
}
