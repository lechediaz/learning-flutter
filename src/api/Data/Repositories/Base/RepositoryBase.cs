using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace api.Data.Repositories.Base
{
    /// <summary>
    /// Implements all the generic methods to use for manage entities
    /// </summary>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    /// <typeparam name="TContext">Type of DbContext</typeparam>
    public abstract class RepositoryBase<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        private readonly DbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        protected RepositoryBase(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            dbSet.Add(entity);

            await dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            dbSet.Remove(entity);

            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);

            await DeleteAsync(entity);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> conditions) =>
            await dbSet.AnyAsync(conditions);

        public virtual async Task<List<TEntity>> GetAsync() =>
            await dbSet.ToListAsync();

        public virtual async Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> whereCondition = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (whereCondition != null)
            {
                query = query.Where(whereCondition);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public virtual async Task<TEntity> GetAsync(int id) =>
            await dbSet.FindAsync(id);

        public virtual async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> whereCondition)
            => await dbSet.FirstOrDefaultAsync(whereCondition);

        public virtual async Task UpdateAsync(TEntity entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;

            await dbContext.SaveChangesAsync();
        }
    }
}
