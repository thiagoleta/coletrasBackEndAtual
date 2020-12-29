using Projeto.Data.Entities;
using Projeto.Data.Extensions;
using Projeto.Data.Repository.Sorts;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Contracts
{
  public interface IMotoristaRepository : IBaseRepository<Motorista>
    {
        CommandResult<PaginatedQueryResult<Motorista>> ObterPaginado(int pagina, int quantidade, MotoristaSort sort, bool ascending, string nome);

        CommandResult<IReadOnlyCollection<Motorista>> Obter(MotoristaSort sort, bool ascending, string nome);
    }
}
