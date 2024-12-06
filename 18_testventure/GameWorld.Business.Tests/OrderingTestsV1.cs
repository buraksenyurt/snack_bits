
namespace GameWorld.Business.Tests;

public class OrderingTestsV1
{
    [Theory]
    [InlineData("Beginner", 10.0, 10.0)]
    [InlineData("Pro", 10.0, 9.0)]
    public void CalculateTotal_ShouldApplyDiscountBasedOnCustomerType(string subscriberType, double orderTotal, double expectedTotal)
    {
        // Arrange
        var order = new Order
        {
            Total = (decimal)orderTotal,
            SubscriberType = subscriberType
        };
        var service = new OrderService();

        // Act
        var total = service.CalculateTotal(order);

        // Assert
        Assert.Equal((decimal)expectedTotal, total);
    }
}