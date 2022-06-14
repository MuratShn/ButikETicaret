using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public class EfEntityRepositoryBase<T, TContext> : IEntitiyRepository<T>
        where T : class, IEntitiy, new()
        where TContext : DbContext, new()
    {
        public void Add(T Entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(Entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(T Entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(Entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<T>().SingleOrDefault(filter);
            }
        }
        public List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                //if (filter == null)
                //    return context.Set<Product>().ToList();
                //else
                //{

                //}
                //Nullsa burası                                      // Değilse burası where filter ile filtreyi uyguluyoruz 
                return filter == null
                    ? context.Set<T>().ToList()
                    : context.Set<T>().Where(filter).ToList();
            }
        }

        public void Update(T Entity)
        {

            using (TContext context = new()) //içinde yazılan nesneler bittiğinde bellekten anında atılır
            {
                var updateddEntity = context.Entry(Entity);
                updateddEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
