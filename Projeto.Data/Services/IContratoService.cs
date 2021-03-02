using Projeto.Data.Commands;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Services
{
    public interface IContratoService
    {
        CommandResult Criar(CriarContratoCommand command);
        CommandResult Remover(int cod_Contrato);
        CommandResult Atualizar(AtualizarContratoCommand command);
    }
}
