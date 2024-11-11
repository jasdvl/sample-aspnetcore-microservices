using HomeAnalytica.DataCollection.Data.Context;
using HomeAnalytica.DataCollection.Data.Entities;
using HomeAnalytica.DataCollection.Data.Repositories;

namespace HomeAnalytica.DataCollection.Data.Infrastructure;

/// <summary>
/// Represents the interface for the Unit of Work pattern, providing access to repositories and saving changes.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Gets the repository for SensorData entities.
    /// </summary>
    IRepository<SensorData> SensorDataRepository { get; }

    /// <summary>
    /// Asynchronously saves all changes made in this unit of work to the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    Task SaveAsync();
}

/// <summary>
/// Implements the Unit of Work pattern, managing the overall database transaction and repository instances.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly HomeAnalyticaDbContext _context;

    private IRepository<SensorData> _sensorDataRepository;

    private bool _disposed = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UnitOfWork(HomeAnalyticaDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets the repository for SensorData entities. Creates a new instance if it doesn't exist.
    /// </summary>
    public IRepository<SensorData> SensorDataRepository {
        get {
            if (_sensorDataRepository == null)
            {
                _sensorDataRepository = new Repository<SensorData>(_context);
            }
            return _sensorDataRepository;
        }
    }

    /// <summary>
    /// Asynchronously saves all changes made in this unit of work to the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    // TODO: Implement IDisposable pattern if needed
}
