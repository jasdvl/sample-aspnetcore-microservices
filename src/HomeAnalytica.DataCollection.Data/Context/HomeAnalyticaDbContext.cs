using HomeAnalytica.DataCollection.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeAnalytica.DataCollection.Data.Context;

public class HomeAnalyticaDbContext : DbContext
{
    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="HomeAnalyticaDbContext"/>-Klasse.
    /// </summary>
    /// <param name="options">Eine Instanz von <see cref="DbContextOptions{HomeAnalyticaDbContext}"/>, die für die Konfiguration des Datenbankkontexts verwendet wird.</param>
    public HomeAnalyticaDbContext(DbContextOptions<HomeAnalyticaDbContext> options) : base(options)
    {
    }

    public DbSet<SensorData> SensorData { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        optionsBuilder.UseNpgsql("Host=my_host;Database=my_db;Username=my_user;Password=my_pw");
    //    }
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        DatabaseSchemaFormatter.DbObjectNamesToSnakeCase(modelBuilder);

        modelBuilder.Entity<SensorData>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Humidity)
                .IsRequired();

            entity.Property(e => e.EnergyConsumption)
                .IsRequired();

            entity.Property(e => e.Temperature)
                .IsRequired();

            // Postgres: CURRENT_TIMESTAMP
        });
    }
}
