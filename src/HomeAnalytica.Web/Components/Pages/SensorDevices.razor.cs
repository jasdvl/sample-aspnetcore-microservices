using HomeAnalytica.Common.Const;
using HomeAnalytica.Common.DTOs;
using HomeAnalytica.Web.Services;
using Microsoft.AspNetCore.Components;

namespace HomeAnalytica.Web.Components.Pages;

public partial class SensorDevices : ComponentBase
{
    private long? _deviceId = null;

    private string _serialNo = string.Empty;

    private SensorType _sensorType = SensorType.Unknown;

    private string? _name;

    private string? _description;

    private string? _location;

    private DateTime? _installationDate;

    private DateTime? _lastMaintenance;

    private bool isDataSubmitted = false;

    public string ErrorMessage { get; set; } = string.Empty;

    private List<SensorDeviceDto> _sensorDevices { get; set; } = new();

    [Inject]
    private ISensorDeviceService SensorDeviceService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _sensorDevices = await LoadSensorDevicesAsync();
    }

    private async Task SubmitSensorDeviceDataAndRefreshDeviceList()
    {
        await SubmitSensorDeviceDataAsync();
        _sensorDevices = await LoadSensorDevicesAsync();
    }

    private async Task SubmitSensorDeviceDataAsync()
    {
        ErrorMessage = string.Empty;

        var data = new SensorDeviceDto
        {
            SerialNo = _serialNo,
            Name = _name,
            Type = _sensorType,
            InstallationDate = _installationDate.HasValue ? DateTime.SpecifyKind(_installationDate.Value, DateTimeKind.Utc) : null,
            LastMaintenance = _lastMaintenance.HasValue ? DateTime.SpecifyKind(_lastMaintenance.Value, DateTimeKind.Utc) : null,
            Status = "Active",
            Description = _description,
            Location = _location
        };

        try
        {
            await SensorDeviceService.PostSensorDeviceAsync(data);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    private async Task<List<SensorDeviceDto>> LoadSensorDevicesAsync()
    {
        var devices = await SensorDeviceService.GetSensorDevicesAsync();

        return devices;
    }
}
