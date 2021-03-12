using Projeto.Data.Entities;
using Projeto.Data.Extensions;
using Projeto.Data.Repository.Sorts;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Contracts
{
   public interface IPagamentoRepository 
    {

        CommandResult<PaginatedQueryResult<Pagamento>> ObterPaginado(int pagina, int quantidade, PagamentoSort sort, bool ascending);

        CommandResult<IReadOnlyCollection<Pagamento>> Obter(PagamentoSort sort, bool ascending);
    }
}
