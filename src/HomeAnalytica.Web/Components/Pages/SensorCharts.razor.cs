using ApexCharts;
using HomeAnalytica.Common.Const;
using HomeAnalytica.Common.DTOs;
using HomeAnalytica.Web.Services;
using Microsoft.AspNetCore.Components;

namespace HomeAnalytica.Web.Components.Pages;

public partial class SensorCharts : ComponentBase
{
    [Inject]
    private ISensorDeviceService SensorDeviceService { get; set; } = default!;

    [Inject]
    private ISensorDataService SensorDataService { get; set; } = default!;

    private List<SensorDeviceDto> _sensorDevices { get; set; } = new();

    private List<SensorDataDto> ChartData = new();

    private ApexChartOptions<SensorDataDto> ChartOptions;

    private string SelectedSensorName { get; set; } = default!;

    private ApexChart<SensorDataDto>? ChartRef;

    private Dictionary<SensorType, string> ChartSeriesNames = new()
                                                                {
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
        ChartData.Clear();
        ChartData.AddRange(sensorData);

        if (ChartOptions == null)
        {
            ChartOptions = new ApexChartOptions<SensorDataDto>
            {
                Chart = new Chart
                {
                    Toolbar = new Toolbar { Show = false },
                    DropShadow = new DropShadow
                    {
                        Enabled = true,
                        Blur = 5,
                        Opacity = 0.3
                    }
                },
                Stroke = new Stroke { Curve = Curve.MonotoneCubic },
                DataLabels = new ApexCharts.DataLabels { Enabled = true },
                Xaxis = new XAxis
                {
                    Type = XAxisType.Datetime,
                    Labels = new XAxisLabels
                    {
                        Format = "hh:mm tt",
                        Rotate = -45
                    }
                },
                Yaxis = new List<YAxis>() { new YAxis() { Title = new AxisTitle() { Text = "Sensor Value" } } },
                Markers = new Markers { Shape = ShapeEnum.Circle, Size = 6, Colors = "#2e3f78", FillOpacity = new Opacity(0.8d) },
                Tooltip = new Tooltip
                {
                    Enabled = true,
                    X = new TooltipX
                    {
                        Formatter = "function(value) { " +
                            "  const options = { year: 'numeric', month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit', hour12: true }; " +
                            "  return new Intl.DateTimeFormat('en-US', options).format(new Date(value)); " +
                            "}"
                    }
                },
            };
        }
    }

    private async Task OnSensorSelectedAsync(ChangeEventArgs e)
    {
        if (long.TryParse(e.Value?.ToString(), out long deviceId))
        {
            var selectedDeviceId = deviceId;
            var selectedSensor = _sensorDevices.First(s => s.Id == selectedDeviceId);
            SelectedSensorName = selectedSensor.Name;

            List<SensorDataDto> sensorData = await LoadSensorDataAsync(selectedSensor.Type, selectedDeviceId);

            TransformToChartData(selectedSensor.Type, sensorData);

            if (ChartRef != null)
            {
                await InvokeAsync(StateHasChanged);

                //await ChartRef.UpdateOptionsAsync(true, true, false);
                await ChartRef.UpdateSeriesAsync(true);
            }
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
