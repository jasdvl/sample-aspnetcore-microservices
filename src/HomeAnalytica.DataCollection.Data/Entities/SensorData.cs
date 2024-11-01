using System.ComponentModel.DataAnnotations;

namespace HomeAnalytica.DataCollection.Data.Entities;

public class SensorData
{
    [Key]
    public long Id { get; set; }

    public double Temperature { get; set; }

    public double Humidity { get; set; }

    public double EnergyConsumption { get; set; }
}
