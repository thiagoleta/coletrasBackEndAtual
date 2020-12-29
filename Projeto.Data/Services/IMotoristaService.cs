using Projeto.Data.Commands;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Services
{
    public interface IMotoristaService
    {
        CommandResult Criar(CriarMotoristaCommand command);
        CommandResult Remover(int cod_Motorista);
        CommandResult Atualizar(AtualizarMotoristaCommand command);
    }
}
