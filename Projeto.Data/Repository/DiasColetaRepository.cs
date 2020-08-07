using Projeto.Data.Context;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Repository
{
  public class DiasColetaRepository : BaseRepository<Dias_Coleta>, IDiasColetaRepository
    {
        private readonly DataColetrans dataContext;

        public DiasColetaRepository(DataColetrans dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }
    }
}
