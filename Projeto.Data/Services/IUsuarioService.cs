using Projeto.Data.Commands;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Services
{
  public interface IUsuarioService
    {
        CommandResult Criar(CriarUsuarioCommand command);

        CommandResult Atualizar(AtualizarUsuarioCommand command);

        CommandResult Remover(int cod_Usuario);
    }
}
