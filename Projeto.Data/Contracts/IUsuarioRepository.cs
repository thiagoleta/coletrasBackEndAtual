using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Contracts
{
   public interface IUsuarioRepository : IBaseDapperRepository<Usuario>
    {
        Usuario Obter(string email);
        Usuario Obter(string email, string senha);

    }
}
