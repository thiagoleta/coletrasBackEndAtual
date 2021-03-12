using Projeto.Data.Commands;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Services
{
    public interface IPagamentoService
    {
        CommandResult Criar(CriarPagamentoCommand command);
        CommandResult Remover(int cod_Pagamento);
        CommandResult Atualizar(AtualizarPagamentoCommand command);
    }
}
