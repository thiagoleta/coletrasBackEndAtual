using Projeto.Data.Commands;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Services
{
   public interface IFrotaService
    {
        CommandResult Criar(CriarFrotaCommand command);

        CommandResult Atualizar(AtualizarFrotaCommand command);

        CommandResult Remover(int cod_Frota);
    }
}
