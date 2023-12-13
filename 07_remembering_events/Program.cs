namespace WeathberBank;

static class Program
{
    static void Main()
    {

    }
}

struct WarningConfiguration
{
    public double HeatAlert;
    public double HumidityAlert;
    public WarningConfiguration(double heatAlert, double humidityAlert)
    {
        HeatAlert=heatAlert;
        HumidityAlert=humidityAlert;
    }
}

public enum WeatherType
{
    Sunny,
    Rainy,
    Cloudy,
    Snowy,
    Stormy
}

class Location
{
    private readonly WarningConfiguration _warningConfiguration;
    private double heat;
    private double humidity;
    public event OverloadEventHandler HeatOverloaded;
    public event OverloadEventHandler HumidityOverloaded;
    public string Title { get; set; }
    public WeatherType WeatherType { get; set; }
    public Location(WarningConfiguration warningConfiguration, string title, WeatherType weatherType)
    {
        _warningConfiguration = warningConfiguration;
        Title = title;
        WeatherType = weatherType;
    }
    public double Heat
    {
        get
        {
            return heat;
        }
        set
        {
            if (value > _warningConfiguration.HeatAlert
                && HeatOverloaded != null)
            {
                HeatOverloaded(this, new OverloadEventArgs { Value = value, AlertType = AlertType.Heat });
            }
            heat = value;
        }
    }
    public double Humidity
    {
        get
        {
            return humidity;
        }
        set
        {
            if (value > _warningConfiguration.HumidityAlert
                && HumidityOverloaded != null)
            {
                HumidityOverloaded(this, new OverloadEventArgs { Value = value, AlertType = AlertType.Humidity });
            }
            humidity = value;
        }
    }
}

public enum AlertType
{
    Heat,
    Humidity
}
public class OverloadEventArgs
{
    public DateTime AlertTime { get; } = DateTime.Now;
    public double Value { get; set; }
    public AlertType AlertType { get; set; }
}

public delegate void OverloadEventHandler(object sender, OverloadEventArgs eventArgs);