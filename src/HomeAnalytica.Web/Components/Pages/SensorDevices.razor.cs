using HomeAnalytica.Common.Const;
using HomeAnalytica.Common.DTOs;
using HomeAnalytica.Web.Services;
using Microsoft.AspNetCore.Components;

namespace HomeAnalytica.Web.Components.Pages;

public partial class SensorDevices : ComponentBase
{
    private string _deviceId = string.Empty;

    private SensorType _sensorType = SensorType.Unknown;

    private string? _name;

    private string? _description;

    private string? _location;

    private DateTime? _installationDate;

    private DateTime? _lastMaintenance;

    private bool isDataSubmitted = false;

    private List<SensorMetadataDto> _sensorDevices { get; set; } = new();

    [Inject]
    private ISensorDeviceService SensorDeviceService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _sensorDevices = await LoadSensorDevicesAsync();
    }

    private async Task SubmitSensorDeviceDataAndRefreshDeviceList()
    {
        await SubmitSensorDeviceDataAsync();
        await LoadSensorDevicesAsync();
    }

    private async Task SubmitSensorDeviceDataAsync()
    {
        var data = new SensorMetadataDto
        {
            DeviceId = _deviceId,
            Name = _name,
            Type = _sensorType,
            InstallationDate = _installationDate.HasValue ? DateTime.SpecifyKind(_installationDate.Value, DateTimeKind.Utc) : null,
            LastMaintenance = _lastMaintenance.HasValue ? DateTime.SpecifyKind(_lastMaintenance.Value, DateTimeKind.Utc) : null,
            Status = "Active",
            Description = _description,
            Location = _location
        };

        var client = HttpClientFactory.CreateClient("YarpClient");

        var response = await client.PostAsJsonAsync("/sensor-devices/post", data);

        if (response.IsSuccessStatusCode)
        {
            isDataSubmitted = true;
        }
        else
        {
            // Handle error
        }
    }

    private async Task<List<SensorMetadataDto>> LoadSensorDevicesAsync()
    {
        var devices = await SensorDeviceService.GetSensorDevicesAsync();

        return devices;
    }
}
