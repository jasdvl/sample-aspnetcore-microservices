using HomeAnalytica.DataCollection.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeAnalytica.DataCollection.Data.Context;

/// <summary>
/// Represents the database context for the HomeAnalytica application, handling interactions with the database.
/// </summary>
public class HomeAnalyticaDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HomeAnalyticaDbContext"/> class.
    /// </summary>
    /// <param name="options">An instance of <see cref="DbContextOptions{HomeAnalyticaDbContext}"/> used to configure the database context.</param>
    public HomeAnalyticaDbContext(DbContextOptions<HomeAnalyticaDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{SensorData}"/> for accessing and managing <see cref="HomeAnalytica.DataCollection.Data.Entities.SensorData"/> entities in the database.
    /// </summary>
    public DbSet<SensorData> SensorData { get; set; } = null!;

    // Uncomment and configure if needed to set up the database connection explicitly.
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        optionsBuilder.UseNpgsql("Host=my_host;Database=my_db;Username=my_user;Password=my_pw");
    //    }
    //}

    /// <summary>
    /// Configures the schema needed for the database using the provided <see cref="ModelBuilder"/>.
    /// </summary>
    /// <param name="modelBuilder">The <see cref="ModelBuilder"/> used to configure entity relationships and properties.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Applies a custom naming convention to convert database object names to snake_case.
        DatabaseSchemaFormatter.DbObjectNamesToSnakeCase(modelBuilder);

        modelBuilder.Entity<SensorData>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Timestamp)
                .IsRequired();
            entity.Property(e => e.Humidity)
                .IsRequired();
            entity.Property(e => e.EnergyConsumption)
                .IsRequired();
            entity.Property(e => e.Temperature)
                .IsRequired();
        });
    }
}
