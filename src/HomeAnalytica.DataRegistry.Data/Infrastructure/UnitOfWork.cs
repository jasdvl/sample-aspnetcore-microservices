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
    IRepository<SensorMetadata> SensorMetadataRepository { get; }

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

    private IRepository<SensorMetadata> _sensorMetadataRepository;

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
    public IRepository<SensorMetadata> SensorMetadataRepository {
        get {
            if (_sensorMetadataRepository == null)
            {
                _sensorMetadataRepository = new Repository<SensorMetadata>(_context);
            }
            return _sensorMetadataRepository;
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
