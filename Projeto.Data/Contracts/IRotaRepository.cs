using Projeto.Data.Entities;
using Projeto.Data.Extensions;
using Projeto.Data.Repository.Sorts;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Contracts
{
   public interface IRotaRepository 
    {
        CommandResult<PaginatedQueryResult<Rota>> ObterPaginado(int pagina, int quantidade, RotaSort sort, bool ascending, string nome);

        CommandResult<IReadOnlyCollection<Rota>> Obter(RotaSort sort, bool ascending, string nome);
    }
}
