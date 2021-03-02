using Projeto.Data.Entities;
using Projeto.Data.Extensions;
using Projeto.Data.Repository.Sorts;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Contracts
{
   public interface IMesReferenciaRepository : IBaseRepository<MesReferencia>
    {
        CommandResult<PaginatedQueryResult<MesReferencia>> ObterPaginado(int pagina, int quantidade, MesReferenciaSort sort, bool ascending, string mesAno);

        CommandResult<IReadOnlyCollection<MesReferencia>> Obter(MesReferenciaSort sort, bool ascending, string mesAno);

    }
}
