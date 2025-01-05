using System.Linq.Expressions;

namespace HomeAnalytica.DataRegistry.Data.Repositories;

/// <summary>
/// Generic repository interface for handling data operations on a specified entity type.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Asynchronously deletes an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity to be deleted.</param>
    Task DeleteAsync(object id);

    /// <summary>
    /// Deletes a specified entity.
    /// </summary>
    /// <param name="entityToDelete">The entity to be deleted.</param>
    void Delete(TEntity entityToDelete);

    /// <summary>
    /// Asynchronously retrieves a collection of entities based on specified criteria.
    /// </summary>
    /// <param name="filter">A function to filter the results.</param>
    /// <param name="orderBy">A function to order the results.</param>
    /// <param name="includeProperties">An array of expressions that specify which navigation properties to include.</param>
    /// <returns>A collection of entities that match the specified criteria.</returns>
    Task<IEnumerable<TEntity>> GetAsync(
                                        Expression<Func<TEntity, bool>>? filter = null,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                        params Expression<Func<TEntity, object>>[] includeProperties);

    /// <summary>
    /// Asynchronously retrieves an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation, containing the entity if found; otherwise, null.</returns>
    Task<TEntity> GetByIDAsync(object id);

    /// <summary>
    /// Asynchronously inserts a new entity.
    /// </summary>
    /// <param name="entity">The entity to be inserted.</param>
    Task InsertAsync(TEntity entity);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entityToUpdate">The entity to be updated.</param>
    void Update(TEntity entityToUpdate);
}
