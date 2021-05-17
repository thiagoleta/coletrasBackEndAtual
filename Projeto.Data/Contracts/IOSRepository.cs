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
   public interface IOSRepository
    {
        CommandResult<IReadOnlyCollection<OS>> Obter(OsSort sort, bool ascending, DataString? nomeCompleto_RazaoSocial, DataString? mesAno);

        CommandResult<PaginatedQueryResult<OS>> ObterPaginado(OsSort sort, bool ascending, int pagina, int quantidade, DataString? nomeCompleto_RazaoSocial, DataString? mesAno);
        
    }
}
