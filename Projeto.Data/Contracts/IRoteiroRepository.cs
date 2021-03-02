using Projeto.Data.Entities;
using Projeto.Data.Extensions;
using Projeto.Data.Repository.Sorts;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Contracts
{
   public interface IRoteiroRepository 
    {
        CommandResult<PaginatedQueryResult<Roteiro>> ObterPaginado(int pagina, int quantidade, RoteiroSort sort, bool ascending);

        CommandResult<IReadOnlyCollection<Roteiro>> Obter(RoteiroSort sort, bool ascending);
    }
}
