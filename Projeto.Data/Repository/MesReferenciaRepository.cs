using Microsoft.EntityFrameworkCore;
using Projeto.Data.Context;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto.Data.Repository
{
   public class MesReferenciaRepository : BaseRepository<MesReferencia>, IMesReferenciaRepository
    {
        private readonly DataColetrans dataContext;

        public MesReferenciaRepository(DataColetrans dataContext) : base(dataContext) //construtor da classe pai..
        {
            this.dataContext = dataContext;
        }
      
        
    }
}

