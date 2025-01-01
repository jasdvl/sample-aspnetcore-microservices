using ApexCharts;
using HomeAnalytica.Common.Const;
using HomeAnalytica.Common.DTOs;
using HomeAnalytica.Web.Services;
using Microsoft.AspNetCore.Components;

namespace HomeAnalytica.Web.Components.Pages;

public partial class SensorCharts : ComponentBase
{
    /// <summary>
    /// Gets or sets the service for interacting with sensor devices.
    /// </summary>
    [Inject]
    private ISensorDeviceService SensorDeviceService { get; set; } = default!;

    /// <summary>
    /// Gets or sets the service that handles sensor data operations, including retrieving and processing sensor data.
    /// </summary>
    [Inject]
    private ISensorDataService SensorDataService { get; set; } = default!;

    private List<SensorDeviceDto> _sensorDevices { get; set; } = new();

    private string ChartTitle = string.Empty;

    private ApexChart<SensorDataDto>? ChartRef;

    private List<SensorDataDto> ChartData = new();

    private SeriesType SeriesType;

    private SeriesStroke? SeriesStroke;

    private ApexChartOptions<SensorDataDto>? ChartOptions;

    private string SelectedSensorName { get; set; } = default!;

    private Dictionary<SensorType, string> ChartSeriesUnits = new()
                                                                {
                                                                    { SensorType.Temperature, "°C" },
                                                                    { SensorType.Humidity, "%" },
                                                                    { SensorType.EnergyConsumption, "kWh" }
                                                                };

    /// <summary>
    /// Invoked when the component is initialized.  
    /// Loads the sensor devices and configures the chart options.
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        await LoadSensorDevicesAsync();
        ChartOptions = CreateChartOptions();
    }

    /// <summary>
    /// Asynchronously loads the list of sensor devices using the SensorDeviceService.
    /// </summary>
    private async Task LoadSensorDevicesAsync()
    {
        _sensorDevices = await SensorDeviceService.GetSensorDevicesAsync();
    }

    private async Task<List<SensorDataDto>> LoadSensorDataAsync(SensorType sensorType, long deviceId)
    {
        var sensorData = await SensorDataService.GetSensorDataAsync(sensorType, deviceId);
        return sensorData;
    }

    /// <summary>
    /// Updates the chart data and configures the chart options based on the provided sensor type.
    /// It sets the appropriate chart series type (line or bar) and applies relevant styling to the chart.
    /// </summary>
    /// <param name="sensorType">The type of sensor data (e.g., EnergyConsumption, Temperature) used to determine chart configuration.</param>
    /// <param name="sensorData">The sensor data to be displayed in the chart, containing timestamps and corresponding values.</param>
    /// <remarks>
    /// - For <see cref="SensorType.EnergyConsumption"/>, the chart will display a bar chart with customized styling (e.g., column width and rounded corners).
    /// - For other sensor types, a line chart will be used with different styling.
    /// - The Y-axis title will be set according to the unit of measurement for the selected sensor type.
    /// </remarks>
    private void UpdateChartDataAndOptions(SensorType sensorType, List<SensorDataDto> sensorData)
    {
        ChartData.Clear();
        ChartData.AddRange(sensorData);
        ChartTitle = sensorType.ToString();

        if (ChartOptions != null)
        {
            if (sensorType == SensorType.EnergyConsumption)
            {
                SeriesType = SeriesType.Bar;
                SeriesStroke = new ApexCharts.SeriesStroke { Color = "#97a9cd", Width = 1 };
                ChartOptions.PlotOptions = new PlotOptions
                {
                    Bar = new PlotOptionsBar
                    {
                        ColumnWidth = "40%",
                        BorderRadius = 5
                    }
                };
            }
            else
            {
                SeriesType = SeriesType.Line;
                SeriesStroke = new ApexCharts.SeriesStroke { Color = "#97a9cd", DashSpace = 3, Width = 5 };
            }

            ChartOptions.Yaxis = new List<YAxis>()
            {
                new YAxis()
                {
                    Title = new AxisTitle()
                    {
                        Text = ChartSeriesUnits[sensorType],
                        Style = new AxisTitleStyle() {FontSize = "18", FontWeight = 600 }
                    }
                }
            };
        }
    }

    /// <summary>
    /// Creates the chart options for the sensor data visualization chart.
    /// </summary>
    /// <returns>
    /// A configured <see cref="ApexChartOptions{SensorDataDto}"/> object containing various chart settings such as title, axes, tooltip, and styling options.
    /// </returns>
    private ApexChartOptions<SensorDataDto> CreateChartOptions()
    {
        ApexChartOptions<SensorDataDto> chartOptions = new ApexChartOptions<SensorDataDto>
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
            Title = new ApexCharts.Title
            {
                Text = ChartTitle,
                Style = new ApexCharts.TitleStyle
                {
                    FontSize = "22",
                    FontWeight = "bold",
                    FontFamily = "Arial, sans-serif"
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
            }
        };

        return chartOptions;
    }

    private async Task OnSensorSelectedAsync(ChangeEventArgs e)
    {
        if (long.TryParse(e.Value?.ToString(), out long deviceId))
        {
            var selectedDeviceId = deviceId;
            var selectedSensor = _sensorDevices.First(s => s.Id == selectedDeviceId);

            if (selectedSensor != null)
            {
                SelectedSensorName = selectedSensor.Name ?? "Unnamed Sensor";

                List<SensorDataDto> sensorData = await LoadSensorDataAsync(selectedSensor.Type, selectedDeviceId);

                UpdateChartDataAndOptions(selectedSensor.Type, sensorData);
                StateHasChanged();
                ChartRef?.UpdateOptionsAsync(true, true, true);
            }
        }
    }
}
