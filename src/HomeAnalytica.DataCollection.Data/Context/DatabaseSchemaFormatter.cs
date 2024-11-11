using HomeAnalytica.DataCollection.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeAnalytica.DataCollection.Data.Context
{
    internal class DatabaseSchemaFormatter
    {
        /// <summary>
        /// Configures entity property names in the model to use snake_case naming convention,
        /// aligning with PostgreSQL's default naming style.
        /// </summary>
        /// <param name="modelBuilder">
        /// The <see cref="ModelBuilder"/> instance used to configure model properties.
        /// </param>
        /// <remarks>
        /// This method explicitly renames the properties of the SensorData entity
        /// to snake_case, matching PostgreSQL's naming conventions for consistency.
        /// </remarks>
        internal static void DbObjectNamesToSnakeCase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SensorData>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("timestamp");

                entity.Property(e => e.Humidity)
                    .HasColumnName("humidity");

                entity.Property(e => e.EnergyConsumption)
                    .HasColumnName("energy_consumption");

                entity.Property(e => e.Temperature)
                    .HasColumnName("temperature");
            });
        }
    }
}
