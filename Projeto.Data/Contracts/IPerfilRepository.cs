using Projeto.Data.Entities;
using Projeto.Data.Extensions;
using Projeto.Data.Repository.Sorts;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Contracts
{
   public interface IPerfilRepository 
    {
        CommandResult<PaginatedQueryResult<Perfil>> ObterPaginado(int pagina, int quantidade, PerfilSort sort, bool ascending);

        CommandResult<IReadOnlyCollection<Perfil>> Obter(PerfilSort sort, bool ascending);
    }
}
