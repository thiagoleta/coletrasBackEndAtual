using Projeto.Data.Entities;
using Projeto.Data.Extensions;
using Projeto.Data.Repository.Sorts;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Contracts
{
   public interface IUsuarioRepository 
    {
        //Usuario Obter(string email);
        Usuario Obter(string email, string senha);

        CommandResult<PaginatedQueryResult<Usuario>> ObterPaginado(int pagina, int quantidade, UsuarioSort sort, bool ascending);

        CommandResult<IReadOnlyCollection<Usuario>> ObterUsuarios(UsuarioSort sort, bool ascending);

        CommandResult<IReadOnlyCollection<Usuario>> ObterUsuariosSelect();

    }
}
