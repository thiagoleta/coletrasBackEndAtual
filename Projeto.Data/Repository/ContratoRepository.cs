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
   public class ContratoRepository : BaseRepository<Contrato>, IContratoRepository
    {
        private readonly DataColetrans dataContext;

        public ContratoRepository(DataColetrans dataContext) : base(dataContext) //construtor da classe pai..
        {
            this.dataContext = dataContext;
        }

        public override List<Contrato> Consultar()
        {
            return dataContext.Contrato              
                .Include(c => c.Cliente)
                .ToList();
        }

        public override Contrato ObterPorId(int id)
        {
            return dataContext.Contrato               
                .Include(c => c.Cliente)
                .FirstOrDefault(con => con.Cod_Contrato == id );
        }
    }
}

