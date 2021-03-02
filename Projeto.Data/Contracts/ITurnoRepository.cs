using Projeto.Data.Entities;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Contracts
{
   public interface ITurnoRepository 
    {
        CommandResult<IReadOnlyCollection<Turno>> ObterTurnos();
    }
}
