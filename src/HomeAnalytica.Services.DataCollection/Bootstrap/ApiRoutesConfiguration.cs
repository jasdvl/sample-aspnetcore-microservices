using HomeAnalytica.Services.DataCollection.DTOs;

namespace HomeAnalytica.Services.DataCollection.Bootstrap;

public static class ApiRoutesConfiguration
{
    public static void ConfigureRoutes(this WebApplication app)
    {
        // Define the "data/submit" endpoint
        app.MapPost("/data/submit", async (SensorData data) =>
        {
            Console.WriteLine($"Received data: Temp={data.Temperature}, Humidity={data.Humidity}, Energy={data.EnergyConsumption}");

            return Results.Ok("Data received successfully");
        });
    }
}
