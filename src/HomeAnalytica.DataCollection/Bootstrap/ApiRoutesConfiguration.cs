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

            await sensorDataService.ProcessSensorDataAsync(data);

            return Results.Ok("Data received successfully");
        });
    }
}
