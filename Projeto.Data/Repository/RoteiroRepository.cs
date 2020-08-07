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
   public class RoteiroRepository : BaseRepository<Roteiro>, IRoteiroRepository
    {
        private readonly DataColetrans dataContext;

        public RoteiroRepository(DataColetrans dataContext) : base(dataContext) //construtor da classe pai..
        {
            this.dataContext = dataContext;
        }

        public override List<Roteiro> Consultar()
        {
            return dataContext.Roteiro               
               .Include(c => c.Cliente)
               .Include(t => t.Turno)
               .Include(d => d.Dias_Coleta)
               .Include(r => r.Rota)
               .Include(m => m.Motorista)
               .Include(ma => ma.Material)
               .ToList();
        }

        public override Roteiro ObterPorId(int id)
        {
            return dataContext.Roteiro
               .Include(c => c.Cliente)
               .Include(t => t.Turno)
               .Include(d => d.Dias_Coleta)
               .Include(r => r.Rota)
               .Include(m => m.Motorista)
               .Include(ma => ma.Material)
               .Include(r => r.Cod_Roteiro)
               .FirstOrDefault(r => r.Cod_Roteiro == id);
        }
    }
}
