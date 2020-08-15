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
   public class RotaRepository : BaseRepository<Rota>, IRotaRepository
    {
        private readonly DataColetrans dataContext;

        public RotaRepository(DataColetrans dataContext) : base(dataContext) //construtor da classe pai..
        {
            this.dataContext = dataContext;
        }

        ////sobrescrita de método (OVERRIDE)
        //public override List<Rota> Consultar()
        //{
        //    //retornar uma consulta de Rota 
        //    //fazendo JOIN com a entidade Motorista
        //    return dataContext.Rota
        //            .Include(p => p.Motorista) //JOIN..
        //            .ToList();
        //}

        //public override Rota ObterPorId(int id)
        //{
        //    //retornar uma consulta de Rota 
        //    //fazendo JOIN com a entidade Motorista
        //    return dataContext.Rota
        //            .Include(p => p.Motorista) //JOIN..
        //            .FirstOrDefault(p => p.Cod_Rota == id);
        //}

        
    }
}

