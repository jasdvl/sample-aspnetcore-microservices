using HomeAnalytica.DataRegistry.Data.Context;
using HomeAnalytica.DataRegistry.Data.Entities;

namespace HomeAnalytica.DataRegistry.Data.Repositories;

/// <summary>
/// A repository class for handling data operations related to <see cref="SensorMetadata"/> entities.
/// Inherits from the generic <see cref="Repository{TEntity}"/> class.
/// </summary>
internal class SensorMetadataRepository : Repository<SensorMetadata>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SensorMetadataRepository"/> class.
    /// </summary>
    /// <param name="context">An instance of <see cref="DataRegistryDbContext"/> used for data access.</param>
    public SensorMetadataRepository(DataRegistryDbContext context) : base(context)
    {
    }
}
