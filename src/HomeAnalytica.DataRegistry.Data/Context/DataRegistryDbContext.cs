using HomeAnalytica.DataRegistry.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeAnalytica.DataRegistry.Data.Context;

/// <summary>
/// Represents the database context for HomeAnalytica sensor metadata, handling interactions with the database.
/// </summary>
public class DataRegistryDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataRegistryDbContext"/> class.
    /// </summary>
    /// <param name="options">An instance of <see cref="DbContextOptions{HomeAnalyticaDbContext}"/> used to configure the database context.</param>
    public DataRegistryDbContext(DbContextOptions<DataRegistryDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{SensorData}"/> for accessing and managing <see cref="HomeAnalytica.DataRegistry.Data.Entities.SensorDevice"/> entities in the database.
    /// </summary>
    public DbSet<SensorDevice> SensorDevices { get; set; } = null!;

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

        modelBuilder.Entity<SensorDevice>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.SerialNo)
                .IsRequired();
            entity.Property(e => e.Type)
                .IsRequired();
            entity.Property(e => e.InstallationDate)
                .IsRequired(false);
            entity.Property(e => e.LastMaintenance)
                .IsRequired(false);
            entity.Property(e => e.Name)
                .IsRequired(false);
            entity.Property(e => e.Location)
                .IsRequired(false);
            entity.Property(e => e.InstallationDate)
                .IsRequired(false);
            entity.Property(e => e.Status)
                .IsRequired(false);
            entity.Property(e => e.Description)
                .IsRequired(false);
        });
    }
}
