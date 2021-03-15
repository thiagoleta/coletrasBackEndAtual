using Projeto.Data.Commands;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Services
{
   public interface IPerfilService
    {
        CommandResult Criar(CriarPerfilCommand command);
        CommandResult Remover(int cod_Perfil);
        CommandResult Atualizar(AtualizarPerfilCommand command);
    }
}
