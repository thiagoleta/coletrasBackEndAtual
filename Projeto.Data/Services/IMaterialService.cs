using Projeto.Data.Commands;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Services
{
  public interface IMaterialService
    {
        CommandResult Criar(CriarMaterialCommand command);
        CommandResult Remover(int cod_Material);
        CommandResult Atualizar(AtualizarMaterialCommand command);
    }
}
