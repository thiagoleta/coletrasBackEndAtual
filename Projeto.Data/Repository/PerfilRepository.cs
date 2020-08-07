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
  public class PerfilRepository : BaseRepository<Perfil>, IPerfilRepository
    {
        private readonly DataColetrans dataContext;

        public PerfilRepository(DataColetrans dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public override List<Perfil> Consultar()
        {
            //fazendo JOIN com a entidade Motorista
            return dataContext.Perfil
                    .Include(p => p.Usuario) //JOIN..
                    .ToList();
        }

        public override Perfil ObterPorId(int id)
        {
            return dataContext.Perfil
                 .Include(p => p.Usuario) //JOIN..
                  .FirstOrDefault(p => p.Cod_Perfil == id);


        }
    }
}
