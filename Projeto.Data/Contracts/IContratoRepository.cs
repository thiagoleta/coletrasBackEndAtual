using Projeto.Data.Entities;
using Projeto.Data.Extensions;
using Projeto.Data.Repository.Sorts;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Contracts
{
   public interface IContratoRepository : IBaseRepository<Contrato>
    {
        CommandResult<PaginatedQueryResult<Contrato>> ObterPaginado(int pagina, int quantidade, ContratoSort sort, bool ascending);

        CommandResult<IReadOnlyCollection<Contrato>> Obter(ContratoSort sort, bool ascending);
    }
}
