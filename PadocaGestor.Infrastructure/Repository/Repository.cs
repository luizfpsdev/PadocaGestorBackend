using Microsoft.EntityFrameworkCore;
using PadocaGestor.Infrastructure.Database;
using System.Data;
using System.Linq.Expressions;

namespace PadocaGestor.Infrastructure.Repository
{

    public class Repository<TEntity> where TEntity : class
        {
            internal PadocaContext context;
            internal DbSet<TEntity> dbSet;

            public Repository(PadocaContext context)
            {
                this.context = context;
                this.dbSet = context.Set<TEntity>();
            }

            public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>>? filter = null,
                string includeProperties = "")
            {
                IQueryable<TEntity> query = dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                return query;
            }

            public virtual Task<List<TEntity>> Get(Expression<Func<TEntity, bool>>? filter = null,
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                string includeProperties = "")
            {
                IQueryable<TEntity> query = Query(filter, includeProperties);

                if (orderBy != null)
                {
                    return orderBy(query).ToListAsync();
                }
                else
                {
                    return query.ToListAsync();
                }
            }

            public virtual TEntity? GetByID(object id)
            {
                return dbSet.Find(id);
            }

            public  virtual async Task InsertAsync(TEntity entity)
            {
                await dbSet.AddAsync(entity);
            }

            public virtual void Delete(object id)
            {
                TEntity? entityToDelete = dbSet.Find(id);
                if (entityToDelete is null)
                {
                    return;
                }

                Delete(entityToDelete);
            }

            public virtual void Delete(TEntity entityToDelete)
            {
                if (context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
                dbSet.Remove(entityToDelete);
            }

            public virtual void Update(TEntity entityToUpdate)
            {
                dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;
            }
        }
}
