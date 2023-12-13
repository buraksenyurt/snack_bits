namespace WeathberBank;

static class Program
{
    static void Main()
    {

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