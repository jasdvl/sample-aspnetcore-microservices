using HomeAnalytica.Common.DTOs;
using HomeAnalytica.DataRegistry.Services;

namespace HomeAnalytica.DataRegistry.Bootstrap;

public static class ApiRoutesConfiguration
{
    public static void ConfigureRoutes(this WebApplication app)
    {
        // Define the "sensor-devices/post" endpoint
        app.MapPost("/sensor-devices/post", async (SensorDeviceDto metadata, ISensorDeviceService sensorDeviceService) =>
        {
            Console.WriteLine($"Received metadata: Sensor = {metadata.Name}");

            try
            {
                await sensorDeviceService.ProcessSensorMetadataAsync(metadata);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing sensor data: {ex.Message}");

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Results.Ok("Sensor data successfully processed.");
        });

        app.MapGet("/sensor-devices/get", async (ISensorDeviceService sensorDeviceService) =>
        {
            try
            {
                var sensorDevices = await sensorDeviceService.GetAllSensorDevicesAsync();
                return Results.Ok(sensorDevices);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching sensor metadata: {ex.Message}");
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        });
    }
}
