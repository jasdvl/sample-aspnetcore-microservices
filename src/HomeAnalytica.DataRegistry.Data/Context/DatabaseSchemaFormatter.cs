using HomeAnalytica.DataRegistry.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeAnalytica.DataRegistry.Data.Context
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
            modelBuilder.Entity<PhysicalUnit>(entity =>
            {
                entity.ToTable("phys_units");
                entity.Property(e => e.Id)
                    .HasColumnName("id");
                entity.Property(e => e.Name)
                   .HasColumnName("name");
                entity.Property(e => e.Symbol)
                    .HasColumnName("symbol");
                entity.Property(e => e.Description)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<MeasuredQuantity>(entity =>
            {
                entity.ToTable("measured_quantities");
                entity.Property(e => e.Id)
                    .HasColumnName("id");
                entity.Property(e => e.Name)
                    .HasColumnName("name");
                entity.Property(e => e.Description)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<SensorDevice>(entity =>
            {
                entity.ToTable("sensor_devices");
                entity.Property(e => e.Id)
                    .HasColumnName("id");
                entity.Property(e => e.SerialNo)
                   .HasColumnName("serial_no");
                entity.Property(e => e.MeasuredQuantityId)
                    .HasColumnName("measured_quantity_id");
                entity.Property(e => e.PhysUnitId)
                    .HasColumnName("phys_unit_id");
                entity.Property(e => e.LastMaintenance)
                    .HasColumnName("last_maintenance");
                entity.Property(e => e.InstallationDate)
                    .HasColumnName("installation_date");
                entity.Property(e => e.Name)
                    .HasColumnName("name");
                entity.Property(e => e.Location)
                    .HasColumnName("location");
                entity.Property(e => e.Status)
                    .HasColumnName("status");
                entity.Property(e => e.Description)
                    .HasColumnName("description");
            });
        }
    }
}
