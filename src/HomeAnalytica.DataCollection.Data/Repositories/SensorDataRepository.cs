using HomeAnalytica.DataCollection.Data.Context;
using HomeAnalytica.DataCollection.Data.Entities;

namespace HomeAnalytica.DataCollection.Data.Repositories;

/// <summary>
/// A repository class for handling data operations related to <see cref="SensorData"/> entities.
/// Inherits from the generic <see cref="Repository{TEntity}"/> class.
/// </summary>
internal class SensorDataRepository : Repository<SensorData>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SensorDataRepository"/> class.
    /// </summary>
    /// <param name="context">An instance of <see cref="HomeAnalyticaDbContext"/> used for data access.</param>
    public SensorDataRepository(HomeAnalyticaDbContext context) : base(context)
    {
    }
}
