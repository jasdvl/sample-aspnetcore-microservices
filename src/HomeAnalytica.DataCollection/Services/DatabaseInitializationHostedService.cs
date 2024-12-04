using HomeAnalytica.DataCollection.Data.Services;

namespace HomeAnalytica.DataCollection.Data
{
    /// <summary>
    /// A hosted service that ensures database initialization tasks, such as creating indexes,
    /// are executed when the application starts.
    /// </summary>
    public class DatabaseInitializationHostedService : IHostedService
    {
        private readonly DatabaseInitializationService _databaseInitializationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseInitializationHostedService"/> class.
        /// </summary>
        /// <param name="databaseInitializationService">
        /// The service responsible for database initialization tasks, such as creating indexes.
        /// </param>
        public DatabaseInitializationHostedService(DatabaseInitializationService databaseInitializationService)
        {
            _databaseInitializationService = databaseInitializationService;
        }

        /// <summary>
        /// Triggered when the application host starts. Ensures database indexes are created.
        /// </summary>
        /// <param name="cancellationToken">A token to signal the start operation should be canceled.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _databaseInitializationService.EnsureIndexesAsync();
        }

        /// <summary>
        /// Triggered when the application host is stopping.
        /// No additional cleanup is required for this service.
        /// </summary>
        /// <param name="cancellationToken">A token to signal the stop operation should be canceled.</param>
        /// <returns>A <see cref="Task"/> that represents the completed operation.</returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
