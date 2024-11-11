using System.Linq.Expressions;

namespace HomeAnalytica.DataCollection.Data.Repositories;

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
    /// Asynchronously retrieves entities based on a filter, order, and included properties.
    /// </summary>
    /// <param name="filter">An optional expression to filter the entities.</param>
    /// <param name="orderBy">An optional function to order the entities.</param>
    /// <param name="includeProperties">Comma-separated list of related entities to include.</param>
    /// <returns>A task that represents the asynchronous operation, containing a collection of entities.</returns>
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

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
