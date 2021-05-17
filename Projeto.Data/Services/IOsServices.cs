using Projeto.Data.Commands;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Services
{
   public interface IOsServices
    {
        CommandResult Criar(CriarOsCommand command);
        CommandResult Remover(int cod_Os);
        CommandResult Atualizar(AtualizarOsCommand command);
    }
}
