using HomeAnalytica.DataRegistry.Data.Context;
using HomeAnalytica.DataRegistry.Data.Entities;

namespace HomeAnalytica.DataRegistry.Data.Repositories;

/// <summary>
/// A repository class for handling data operations related to <see cref="SensorDevice"/> entities.
/// Inherits from the generic <see cref="Repository{TEntity}"/> class.
/// </summary>
public class SensorDeviceRepository : Repository<SensorDevice>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SensorDeviceRepository"/> class.
    /// </summary>
    /// <param name="context">An instance of <see cref="DataRegistryDbContext"/> used for data access.</param>
    public SensorDeviceRepository(DataRegistryDbContext context) : base(context)
    {
    }
}
