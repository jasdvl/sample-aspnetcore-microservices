using HomeAnalytica.DataRegistry.Data.Context;
using HomeAnalytica.DataRegistry.Data.Entities;

namespace HomeAnalytica.DataRegistry.Data.Repositories;

/// <summary>
/// A repository class for handling data operations related to <see cref="SensorDevice"/> entities.
/// Inherits from the generic <see cref="Repository{TEntity}"/> class.
/// </summary>
internal class SensorDevicesRepository : Repository<SensorDevice>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SensorDevicesRepository"/> class.
    /// </summary>
    /// <param name="context">An instance of <see cref="DataRegistryDbContext"/> used for data access.</param>
    public SensorDevicesRepository(DataRegistryDbContext context) : base(context)
    {
    }
}
