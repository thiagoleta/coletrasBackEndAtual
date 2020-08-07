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
   public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        private readonly DataColetrans dataContext;

        public ClienteRepository(DataColetrans dataContext) : base(dataContext) //construtor da classe pai..
        {
            this.dataContext = dataContext;
        }

        //public override List<Cliente> Consultar()
        //{
        //    return dataContext.Cliente
        //        .Include(c => c.Rota)//join
        //        .ToList();                 
        //}

        //public override Cliente ObterPorId(int id)
        //{
        //    return dataContext.Cliente
        //        .Include(c => c.Rota)
        //        .FirstOrDefault(c=> c.Cod_Cliente == id);
        //}


    }
}

