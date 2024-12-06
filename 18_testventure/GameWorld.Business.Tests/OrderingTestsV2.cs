
namespace GameWorld.Business.Tests;

public class OrderingTestsV2
{
    [Theory]
    [InlineData(SubscriberType.Beginner, 10.0, 10.0)]
    [InlineData(SubscriberType.Pro, 10.0, 9.0)]
    public void CalculateTotal_ShouldApplyCorrectDiscountBasedOnSubscriberType(SubscriberType subscriberType, double orderTotal, double expectedTotal)
    {
        // Arrange
        var order = new OrderV2
        {
            Total = (decimal)orderTotal,
            SubscriberType = subscriberType
        };
        var service = new OrderServiceV2();

        // Act
        var total = service.CalculateTotal(order);

        // Assert
        Assert.Equal((decimal)expectedTotal, total);
    }
}

public enum SubscriberType
{
    Beginner,
    Pro
}

public interface IDiscountStrategy
{
    decimal Apply(decimal total);
}

public class BeginnerDiscountStrategy : IDiscountStrategy
{
    public decimal Apply(decimal total) => total;
}

public class ProDiscountStrategy : IDiscountStrategy
{
    public decimal Apply(decimal total) => total * 0.9m;
}

public static class DiscountStrategyFactory
{
    public static IDiscountStrategy GetStrategy(SubscriberType subscriberType)
    {
        return subscriberType switch
        {
            SubscriberType.Beginner => new BeginnerDiscountStrategy(),
            SubscriberType.Pro => new ProDiscountStrategy(),
            _ => throw new ArgumentException("Invalid subscriber type")
        };
    }
}

public class OrderV2
{
    public decimal Total { get; set; }
    public DateTime OrderDate { get; } = DateTime.Now;
    public SubscriberType SubscriberType { get; set; }
}

public class OrderServiceV2
{
    public decimal CalculateTotal(OrderV2 order)
    {
        var strategy = DiscountStrategyFactory.GetStrategy(order.SubscriberType);
        return strategy.Apply(order.Total);
    }
}
