using HomeAnalytica.Common.DTOs;
using HomeAnalytica.DataRegistry.Services;

namespace HomeAnalytica.DataRegistry.Bootstrap;

public static class ApiRoutesConfiguration
{
    public static void ConfigureRoutes(this WebApplication app)
    {
        // Define the "data/submit" endpoint
        app.MapPost("/data/submit", async (SensorMetadataDto metadata, ISensorMetadataService sensorDataService) =>
        {
            Console.WriteLine($"Received metadata: Sensor = {metadata.Name}");

            try
            {
                await sensorDataService.ProcessSensorMetadataAsync(metadata);
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
