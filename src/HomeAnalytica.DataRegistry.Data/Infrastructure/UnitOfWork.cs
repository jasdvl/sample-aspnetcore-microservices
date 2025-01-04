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
    /// Gets the repository for <see cref="SensorDevice" /> entities.
    /// </summary>
    SensorDeviceRepository SensorDeviceRepository { get; }

    /// <summary>
    /// Gets the repository for <see cref="MeasuredQuantity"/> entities.
    /// </summary>
    MeasuredQuantityRepository MeasuredQuantityRepository { get; }

    /// <summary>
    /// Gets the repository for <see cref="PhysicalUnit"/> entities.
    /// </summary>
    PhysicalUnitRepository PhysicalUnitRepository { get; }

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

    private SensorDeviceRepository _sensorDeviceRepository;

    private MeasuredQuantityRepository _measuredQuantity;

    private PhysicalUnitRepository _physUnitRepository;

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
    ///  Gets the repository for <see cref="SensorDevice" /> entities. Creates a new instance if it doesn't exist.
    /// </summary>
    public SensorDeviceRepository SensorDeviceRepository {
        get {
            if (_sensorDeviceRepository == null)
            {
                _sensorDeviceRepository = new SensorDeviceRepository(_context);
            }
            return _sensorDeviceRepository;
        }
    }

    /// <summary>
    /// Gets the repository for <see cref="MeasuredQuantity"/> entities. Creates a new instance if it doesn't exist.
    /// </summary>
    public MeasuredQuantityRepository MeasuredQuantityRepository {
        get {
            if (_measuredQuantity == null)
            {
                _measuredQuantity = new MeasuredQuantityRepository(_context);
            }
            return _measuredQuantity;
        }
    }

    /// <summary>
    /// Gets the repository for <see cref="PhysicalUnit"/> entities. Creates a new instance if it doesn't exist.
    /// </summary>
    public PhysicalUnitRepository PhysicalUnitRepository {
        get {
            if (_physUnitRepository == null)
            {
                _physUnitRepository = new PhysicalUnitRepository(_context);
            }
            return _physUnitRepository;
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
