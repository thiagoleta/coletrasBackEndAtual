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
   public class MotoristaRepository : BaseRepository<Motorista>, IMotoristaRepository
    {
        private readonly DataColetrans dataContext;

        public MotoristaRepository(DataColetrans dataContext) : base(dataContext) //construtor da classe pai..
        {
            this.dataContext = dataContext;
        }
        #region MyRegion
        ////sobrescrita de método (OVERRIDE)
        //public override List<Motorista> Consultar()
        //{
        //    //retornar uma consulta de Motorista 
        //    //fazendo JOIN com a entidade Rotas
        //    return dataContext.Motorista
        //            .Include(p => p.Rota) //JOIN..
        //            .ToList();
        //}

        ////sobrescrita de método (OVERRIDE)
        //public override List<Motorista> Consultar(Func<Motorista, bool> where)
        //{
        //    //retornar uma consulta de Motorista 
        //    //JOIN com a entidade Rotas
        //    return dataContext.Motorista
        //            .Include(p => p.Rota) //JOIN..
        //            .Where(where)
        //            .ToList();
        //}

        ////sobrescrita de método (OVERRIDE)
        //public override Motorista Obter(Func<Motorista, bool> where)
        //{
        //    //retornar uma consulta de Produtos fazendo 
        //    //JOIN com a entidade Rotas
        //    return dataContext.Motorista
        //            .Include(p => p.Rota) //JOIN..
        //            .Where(where)
        //            .FirstOrDefault();
        //}

        ////sobrescrita de método (OVERRIDE)
        //public override Motorista ObterPorId(int id)
        //{
        //    //retornar uma consulta de Produtos fazendo 
        //    //JOIN com a entidade Rotas
        //    return dataContext.Motorista
        //            .Include(p => p.Rota) //JOIN..
        //            .FirstOrDefault(p => p.CodMotorista == id);
        //}
        #endregion
    }
}
