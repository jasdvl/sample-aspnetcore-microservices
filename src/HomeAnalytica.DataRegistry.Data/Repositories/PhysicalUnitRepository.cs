using HomeAnalytica.DataRegistry.Data.Context;
using HomeAnalytica.DataRegistry.Data.Entities;

namespace HomeAnalytica.DataRegistry.Data.Repositories;

/// <summary>
/// A repository class for handling data operations related to <see cref="PhysicalUnit"/> entities.
/// Inherits from the generic <see cref="Repository{TEntity}"/> class.
/// </summary>
public class PhysicalUnitRepository : Repository<PhysicalUnit>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PhysicalUnitRepository"/> class.
    /// </summary>
    /// <param name="context">An instance of <see cref="DataRegistryDbContext"/> used for data access.</param>
    public PhysicalUnitRepository(DataRegistryDbContext context) : base(context)
    {
    }
}
