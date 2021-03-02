using Microsoft.EntityFrameworkCore;
using Projeto.Data.Context;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto.Data.Repository
{
    public class TurnoRepository : BaseRepository<Turno>, ITurnoRepository
    {
        private readonly DataColetrans dataContext;

        public TurnoRepository(DataColetrans dataContext) : base(dataContext) //construtor da classe pai..
        {
            this.dataContext = dataContext;
        }

        public CommandResult<IReadOnlyCollection<Turno>> ObterTurnos()
        {
            IQueryable<Turno> query = dataContext.Turno.AsNoTracking();
            var result = query.OrderBy(x => x.Nome_Turno).Select(x => new Turno(x.Cod_Turno, x.Nome_Turno)).ToArray();
            return CommandResult<IReadOnlyCollection<Turno>>.Valid(result);
        }
    }
}
