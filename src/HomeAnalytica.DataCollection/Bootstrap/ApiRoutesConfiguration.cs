using HomeAnalytica.Common.DTOs;
using HomeAnalytica.DataCollection.Services;

namespace HomeAnalytica.DataCollection.Bootstrap;

public static class ApiRoutesConfiguration
{
    public static void ConfigureRoutes(this WebApplication app)
    {
        // Define the "data/submit" endpoint
        app.MapPost("/data/submit", async (SensorDataDto data, ISensorDataService sensorDataService) =>
        {
            Console.WriteLine($"Received data: Temp={data.Temperature}, Humidity={data.Humidity}, Energy={data.EnergyConsumption}");

            try
            {
                await sensorDataService.ProcessSensorDataAsync(data);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing sensor data: {ex.Message}");

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Results.Ok("Sensor data successfully processed.");
        });
    }
}
