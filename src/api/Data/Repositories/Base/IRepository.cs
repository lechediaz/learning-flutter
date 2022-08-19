using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace api.Data.Repositories.Base
{
    /// <summary>
    /// Defines all the generic methods to use for manage entities.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Allows to add a record.
        /// </summary>
        /// <param name="entity">Record to add.</param>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Allows to delete a record.
        /// </summary>
        /// <param name="entity">Record to delete.</param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Allows to delete a record by Id.
        /// </summary>
        /// <param name="id">Record Id to delete.</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Allows to check if there is at least one record that match the search conditions.
        /// </summary>
        /// <param name="conditions">Search conditions</param>
        /// <returns>True if exists at least one record that match the search conditions,
        ///  otherwise false.</returns>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> conditions);

        /// <summary>
        /// Allows to get all the records.
        /// </summary>
        /// <returns>All the records.</returns>
        Task<List<TEntity>> GetAsync();

        /// <summary>
        /// Allows to get all the records that match the search conditions. It also allows to sort the results and bring related entities.
        /// </summary>
        /// <param name="whereCondition">Search condition to find the records.</param>
        /// <param name="orderBy">Defines the order that the results should have.</param>
        /// <param name="includeProperties">Related entities to include.</param>
        /// <returns>Records that match the search conditions.</returns>
        Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> whereCondition = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        /// Get one record by Id.
        /// </summary>
        /// <param name="id">Id to search the record.</param>
        /// <returns>Record found by Id.</returns>
        Task<TEntity> GetAsync(int id);

        /// <summary>
        /// Allows to get the first record that match the conditions.
        /// </summary>
        /// <param name="whereCondition">Conditions to search.</param>
        /// <returns>Record that match the conditions.</returns>
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> whereCondition);

        /// <summary>
        /// Allows to update a record.
        /// </summary>
        /// <param name="entity">Record to update.</param>
        Task UpdateAsync(TEntity entity);
    }
}
