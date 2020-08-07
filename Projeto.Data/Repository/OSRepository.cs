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
   public class OSRepository : BaseRepository<OS>, IOSRepository
    {
        private readonly DataColetrans dataContext;

        public OSRepository(DataColetrans dataContext) : base(dataContext) //construtor da classe pai..
        {
            this.dataContext = dataContext;
        }

        public override List<OS> Consultar()
        {
            return dataContext.OS
                .Include(m => m.MesReferencia)
                .Include(co => co.Contrato).ToList();
                
        }

        public override OS ObterPorId(int id)
        {
            return dataContext.OS
                .Include(m => m.MesReferencia)
                .Include(co => co.Contrato)                
                .Include(c => c.Clientes)
                .FirstOrDefault(o => o.Cod_OS == id);

        }

    }
}

