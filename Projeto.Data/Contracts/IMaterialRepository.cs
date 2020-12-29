using Projeto.Data.Entities;
using Projeto.Data.Extensions;
using Projeto.Data.Repository.Sorts;
using Projeto.Data.Seedwork;
using Projeto.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Contracts
{
   public interface IMaterialRepository 
    {
        CommandResult<PaginatedQueryResult<Material>> ObterPaginado(int pagina, int quantidade,  MaterialSort sort, bool ascending, string descricao);

        CommandResult<IReadOnlyCollection<Material>> Obter(MaterialSort sort, bool ascending, string descricao);
    }
}
