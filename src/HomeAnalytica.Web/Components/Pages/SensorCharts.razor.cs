using HomeAnalytica.Common.Const;
using HomeAnalytica.Common.DTOs;
using HomeAnalytica.Web.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HomeAnalytica.Web.Components.Pages;

public partial class SensorCharts : ComponentBase
{
    [Inject]
    private ISensorDeviceService SensorDeviceService { get; set; } = default!;

    private List<SensorDeviceDto> _sensorDevices { get; set; } = new();

    //default value cannot be 0 -> first selectedindex is 0.
    private int Index = -1;

    public ChartOptions Options = new ChartOptions();

    public List<ChartSeries> ChartData = new();

    // Labels for the x-axis (timestamps)
    private string[] XAxisLabels = Array.Empty<string>();

    private Dictionary<SensorType, string> ChartSeriesNames = new() {
                                                                { SensorType.Temperature, "Temperature (°C)" },
                                                                { SensorType.Humidity, "Rel. Humidity (%)" },
                                                                { SensorType.EnergyConsumption, "Energy Consumption (kWh)" }
                                                                };

    protected override async Task OnInitializedAsync()
    {
        await LoadSensorDevicesAsync();
    }

    // Transform data for the chart
    private void TransformToChartData(SensorType sensorType, List<SensorDataDto> sensorData)
    {
        XAxisLabels = sensorData.Select(d => d.Timestamp.ToString("HH:mm")).ToArray();
        ChartData = new List<ChartSeries>
        {
            new ChartSeries
            {
                Name = ChartSeriesNames[sensorType],
                Data = sensorData.Select(d => d.Value).ToArray()
            }
        };
    }

    private async Task OnSensorSelectedAsync(ChangeEventArgs e)
    {
        if (long.TryParse(e.Value?.ToString(), out long deviceId))
        {
            var selectedDeviceId = deviceId;
            var selectedSensor = _sensorDevices.First(s => s.Id == selectedDeviceId);

            List<SensorDataDto> sensorData = await LoadSensorDataAsync(selectedSensor.Type, selectedDeviceId);

            TransformToChartData(selectedSensor.Type, sensorData);
        }
    }

    private async Task<List<SensorDataDto>> LoadSensorDataAsync(SensorType sensorType, long deviceId)
    {
        var sensorData = await SensorDataService.GetSensorDataAsync(sensorType, deviceId);
        return sensorData;
    }

    private async Task LoadSensorDevicesAsync()
    {
        _sensorDevices = await SensorDeviceService.GetSensorDevicesAsync();
    }
}
