using Projeto.Data.Context;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
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

    }
}
