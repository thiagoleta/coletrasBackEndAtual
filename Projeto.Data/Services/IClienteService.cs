using Projeto.Data.Commands;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Services
{
   public interface IClienteService 
    {
        CommandResult Criar(CriarClienteCommand command);
        CommandResult Remover(int cod_Cliente);
        CommandResult Atualizar(AtualizarClienteCommand command);
    }
}
