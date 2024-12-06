namespace GameWorld.Business.Tests;

public class OrderService
{
    public decimal CalculateTotal(Order order)
    {
        decimal discount = order.SubscriberType == "Pro" ? 0.1M : 0M;
        return order.Total * (1 - discount);
    }
}