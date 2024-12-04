using HomeAnalytica.DataRegistry.Data.Context;
using HomeAnalytica.DataRegistry.Data.Entities;
using HomeAnalytica.DataRegistry.Data.Repositories;

namespace HomeAnalytica.DataRegistry.Data.Infrastructure;

/// <summary>
/// Represents the interface for the Unit of Work pattern, providing access to repositories and saving changes.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Gets the repository for SensorData entities.
    /// </summary>
    IRepository<SensorDevice> SensorDeviceRepository { get; }

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
    private readonly DataRegistryDbContext _context;

    private IRepository<SensorDevice> _sensorDeviceRepository;

    private bool _disposed = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UnitOfWork(DataRegistryDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets the repository for SensorData entities. Creates a new instance if it doesn't exist.
    /// </summary>
    public IRepository<SensorDevice> SensorDeviceRepository {
        get {
            if (_sensorDeviceRepository == null)
            {
                _sensorDeviceRepository = new Repository<SensorDevice>(_context);
            }
            return _sensorDeviceRepository;
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
