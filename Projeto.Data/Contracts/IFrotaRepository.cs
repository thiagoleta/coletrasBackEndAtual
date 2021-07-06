using Projeto.Data.Entities;
using Projeto.Data.Extensions;
using Projeto.Data.Repository.Sorts;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Contracts
{
  public interface IFrotaRepository
    {
        CommandResult<PaginatedQueryResult<Frota>> ObterPaginado(int pagina, int quantidade, FrotaSort sort, bool ascending, string descricao);

        CommandResult<IReadOnlyCollection<Frota>> Obter(FrotaSort sort, bool ascending, string descricao);

        CommandResult<IReadOnlyCollection<Frota>> ObterFrota();
    }
}
