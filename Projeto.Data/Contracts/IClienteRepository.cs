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
   public interface IClienteRepository 
    {        

        CommandResult<IReadOnlyCollection<Cliente>> Obter(ClienteSort sort, bool ascending, DataString? nomeCompleto_RazaoSocial, DataString? cpfCnpj);

        CommandResult<PaginatedQueryResult<Cliente>> ObterPaginado(ClienteSort sort, bool ascending, int pagina, int quantidade, DataString? nomeCompleto_RazaoSocial, DataString? cpfCnpj);

        CommandResult<IReadOnlyCollection<Cliente>> ObterClientesAtivos();

    }
}
