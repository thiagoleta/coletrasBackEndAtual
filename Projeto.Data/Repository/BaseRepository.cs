using Microsoft.EntityFrameworkCore;
using Projeto.Data.Context;
using Projeto.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto.Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
          where T : class

    {
        //atributo para armazenar o contexto do EF
        private readonly DataColetrans dataContext;

        public BaseRepository(DataColetrans dataContext)
        {
            this.dataContext = dataContext;
        }

        public virtual void Alterar(T entity)
        {
            dataContext.Entry(entity).State = EntityState.Modified; //edição
            dataContext.SaveChanges(); //executando
        }

        public virtual void Excluir(T entity)
        {
            dataContext.Entry(entity).State = EntityState.Deleted; //exclusão
            dataContext.SaveChanges(); //executando
        }

        public virtual List<T> Consultar()
        {
            return dataContext.Set<T>().ToList();
        }

        public virtual List<T> Consultar(Func<T, bool> where)
        {
            return dataContext.Set<T>()
                .Where(where)
                .ToList();
        }

        public virtual T Obter(Func<T, bool> where)
        {
            return dataContext.Set<T>()
                .FirstOrDefault(where);
        }
        public virtual T ObterPorId(int id)
        {
            return dataContext.Set<T>()
                .Find(id); //buscar pelo id..
        }

        public virtual void Inserir(T entity)
        {
            dataContext.Entry(entity).State = EntityState.Added; //inserção
            dataContext.SaveChanges(); //executando
        }

    }
}
