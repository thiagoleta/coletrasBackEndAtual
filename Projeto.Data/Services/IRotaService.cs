using Projeto.Data.Commands;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Services
{
    public interface IRotaService
    {
        CommandResult Criar(CriarRotaCommand command);
        CommandResult Remover(int cod_Rota);
        CommandResult Atualizar(AtualizarRotaCommand command);
    }
}
