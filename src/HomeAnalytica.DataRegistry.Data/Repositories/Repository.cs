using HomeAnalytica.DataRegistry.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HomeAnalytica.DataRegistry.Data.Repositories;

/// <summary>
/// Generic repository class that handles CRUD operations for entities.
/// </summary>
/// <typeparam name="TEntity">The type of entity this repository works with.</typeparam>
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    internal DataRegistryDbContext _context;

    internal DbSet<TEntity> _dbSet;

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public Repository(DataRegistryDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    /// <summary>
    /// Asynchronously retrieves a collection of entities based on specified criteria.
    /// </summary>
    /// <param name="filter">A function to filter the results.</param>
    /// <param name="orderBy">A function to order the results.</param>
    /// <param name="includeProperties">An array of expressions that specify which navigation properties to include.</param>
    /// <returns>A collection of entities that match the specified criteria.</returns>
    public virtual async Task<IEnumerable<TEntity>> GetAsync(
                                                            Expression<Func<TEntity, bool>>? filter = null,
                                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                            params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties)
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

    /// <summary>
    /// Asynchronously retrieves an entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity to retrieve.</param>
    /// <returns>The entity with the specified ID, or null if not found.</returns>
    public virtual async Task<TEntity> GetByIDAsync(object id)
    {
        return await _dbSet.FindAsync(id);
    }

    /// <summary>
    /// Asynchronously inserts a new entity into the database.
    /// </summary>
    /// <param name="entity">The entity to insert.</param>
    public virtual async Task InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    /// <summary>
    /// Asynchronously deletes an entity from the database by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    public virtual async Task DeleteAsync(object id)
    {
        TEntity? entityToDelete = await _dbSet.FindAsync(id);

        if (entityToDelete != null)
        {
            Delete(entityToDelete);
        }
    }

    /// <summary>
    /// Deletes an existing entity from the database.
    /// </summary>
    /// <param name="entityToDelete">The entity to delete.</param>
    public virtual void Delete(TEntity entityToDelete)
    {
        if (_context.Entry(entityToDelete).State == EntityState.Detached)
        {
            _dbSet.Attach(entityToDelete);
        }
        _dbSet.Remove(entityToDelete);
    }

    /// <summary>
    /// Updates an existing entity in the database.
    /// </summary>
    /// <param name="entityToUpdate">The entity to update.</param>
    public virtual void Update(TEntity entityToUpdate)
    {
        _dbSet.Attach(entityToUpdate);
        _context.Entry(entityToUpdate).State = EntityState.Modified;
    }
}
