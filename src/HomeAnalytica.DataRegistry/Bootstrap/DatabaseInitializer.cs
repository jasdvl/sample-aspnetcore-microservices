using HomeAnalytica.DataRegistry.Data.Context;
using HomeAnalytica.DataRegistry.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeAnalytica.DataRegistry.Bootstrap
{
    /// <summary>
    /// Responsible for applying database migrations and seeding initial data.
    /// </summary>
    public class DatabaseInitializer
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Applies pending migrations and seeds the database with initial data.
        /// </summary>
        public void Initialize()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DataRegistryDbContext>();

                context.Database.Migrate();

                SeedDatabase(context);
            }
        }

        private void SeedDatabase(DataRegistryDbContext context)
        {
            if (!context.Set<PhysicalUnit>().Any())
            {
                var units = new List<PhysicalUnit>
                {
                    new PhysicalUnit
                    {
                        Id = (int) Common.Const.PhysicalUnit.CelsiusDegrees,
                        Name = "Degree Celsius",
                        Symbol = "°C",
                        Description = "Unit of temperature measurement in the Celsius scale"
                    },
                    new PhysicalUnit
                    {
                        Id = (int) Common.Const.PhysicalUnit.Percent,
                        Name = "Percentage",
                        Symbol = "%",
                        Description = "Unit representing a value as a fraction of 100"
                    },
                    new PhysicalUnit
                    {
                        Id = (int) Common.Const.PhysicalUnit.KiloWattHours,
                        Name = "Kilowatt Hour",
                        Symbol = "kWh",
                        Description = "Unit of energy equivalent to one kilowatt of power consumed for one hour"
                    }
                };


                context.Set<PhysicalUnit>().AddRange(units);
                context.SaveChanges();
            }

            if (!context.Set<MeasuredQuantity>().Any())
            {
                var measuredQuantities = new List<MeasuredQuantity>
                {
                    new MeasuredQuantity
                    {
                        Id = (int) Common.Const.MeasuredQuantity.Temperature,
                        Name = "Temperature",
                        Description = "Temperature, typically measured in Celsius (°C) or Kelvin (K), indicates the average kinetic energy of particles in a substance"
                    },
                    new MeasuredQuantity
                    {
                        Id = (int) Common.Const.MeasuredQuantity.Humidity,
                        Name = "Humidity",
                        Description = "Relative humidity, expressed as a percentage (%), measures the amount of water vapor in the air compared to the maximum possible at a given temperature"
                    },
                    new MeasuredQuantity
                    {
                        Id = (int) Common.Const.MeasuredQuantity.EnergyConsumption,
                        Name = "Energy Consumption",
                        Description = "Energy consumption, typically measured in kilowatt-hours (kWh), refers to the total electrical energy used over time"
                    }
                };

                context.Set<MeasuredQuantity>().AddRange(measuredQuantities);
                context.SaveChanges();
            }
        }
    }
}
