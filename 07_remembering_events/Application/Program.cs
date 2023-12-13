namespace WeathberBank;

static class Program
{
    static void Main()
{
    var randomizer = new Random();
    var weatherTypeValues = Enum.GetValues(typeof(WeatherType));

    var locations = new List<Location>{
        new(new WarningConfiguration(41,80),"İstanbul",(WeatherType)randomizer.Next(weatherTypeValues.Length)),
        new(new WarningConfiguration(36.5,60),"Berlin",(WeatherType)randomizer.Next(weatherTypeValues.Length)),
        new(new WarningConfiguration(38.5,40),"Tokyo",(WeatherType)randomizer.Next(weatherTypeValues.Length)),
    };
    foreach (var location in locations)
    {
        location.HumidityOverloaded += Measurement_Overloaded;
        location.HeatOverloaded += Measurement_Overloaded;
    }

    Console.WriteLine("Rasathane Gözlemevi");

    while (true)
    {
        foreach (var location in locations)
        {
            var heatValue = Utility.GetRandomDouble(1, 45);
            var humidityValue = Utility.GetRandomDouble(10, 99);
            location.Humidity = humidityValue;
            location.Heat = heatValue;
            Thread.Sleep(2000);
        }
    }
}

private static void Measurement_Overloaded(object sender, OverloadEventArgs eventArgs)
{
    var location = (Location)sender;
    Console.WriteLine($"{eventArgs.AlertTime} : {location.Title}({location.WeatherType}) {eventArgs.AlertType} ({eventArgs.Value:F2})");
}
}

static class Utility
{
    public static double GetRandomDouble(double min, double max)
    {
        Random rnd = new();
        var value = rnd.NextDouble();
        return value * (max - min) + min;
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