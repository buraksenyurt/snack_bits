namespace GameWorld.Business.Tests;

public class Order
{
    public decimal Total { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public string SubscriberType { get; set; }
}