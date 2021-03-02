using Projeto.Data.Commands;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Services
{
   public interface IMesReferenciaService
    {
        CommandResult Criar(CriarMesReferenciaCommand command);
        CommandResult Remover(int cod_MesReferencia);
        CommandResult Atualizar(AtualizarMesReferenciaCommand command);
    }
}
