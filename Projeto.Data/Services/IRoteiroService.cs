using Projeto.Data.Commands;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Services
{
    public interface IRoteiroService
    {
        CommandResult Criar(CriarRoteiroCommand command);

        CommandResult Atualizar(AtualizarRoteiroCommand command);

        CommandResult Remover(int cod_Roteiro);
    }
}
