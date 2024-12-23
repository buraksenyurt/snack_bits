# TestVenture İçin Kullanım Kılavuzu

## 01 Gerçek Hayat Kullanım Örneği (TDD Uyumlu)

Yine Red Green Blue durumu ile ilerlenir. Bir sipariş sistemi düşünelim. OrderingTest olarak aşağıdaki UnitTest'i yazabiliriz.

```csharp
public class OrderingTests
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
```

Red durumundan çıkmak Green durumuna geçilir. Bunun için Order ve OrderService isimli sınıflar inşa edilir.

```csharp
public class OrderService
{
    public decimal CalculateTotal(Order order)
    {
        decimal discount = order.SubscriberType == "Pro" ? 0.1M : 0M;
        return order.Total * (1 - discount);
    }
}

public class Order
{
    public decimal Total { get; set; }
    public DateTime OrderDate { get; } = DateTime.Now;
    public string SubscriberType { get; set; }
}
```

Blue kısmında ise refactoring işlemi yapılır ve indirim seçeneği genişletilebilir bir özellik haline getirilir.

```csharp
// SubscriberType bir Enum türüne evrilir
public enum SubscriberType
{
    Beginner,
    Pro
}


// İndirim stratejisi bir arayüzle soyutlanır
public interface IDiscountStrategy
{
    decimal Apply(decimal total);
}

// Stratejiler Uygulanır
public class BeginnerDiscountStrategy : IDiscountStrategy
{
    public decimal Apply(decimal total) => total;
}

public class ProDiscountStrategy : IDiscountStrategy
{
    public decimal ApplyD(decimal total) => total * 0.9m;
}

// Strateji oluşturmak için bir Factory sınıfı kullanılabilir
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

// Var olan sınıflar güncellenir
public class Order
{
    public decimal Total { get; set; }
    public DateTime OrderDate { get; } = DateTime.Now;
    public SubscriberType SubscriberType { get; set; }
}

public class OrderService
{
    public decimal CalculateTotal(Order order)
    {
        var strategy = DiscountStrategyFactory.GetStrategy(order.SubscriberType);
        return strategy.Apply(order.Total);
    }
}

// Bunlara bağlı olarak Test servis metodu güncellenir

public class OrderingTests
{
    [Theory]
    [InlineData(SubscriberType.Beginner, 10.0, 10.0)]
    [InlineData(SubscriberType.Pro, 10.0, 9.0)]
    public void CalculateTotal_ShouldApplyCorrectDiscountBasedOnSubscriberType(SubscriberType subscriberType, double orderTotal, double expectedTotal)
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

```

## 02 Mock Kullanımı

Bu örnekte GameWorld.Business paketindeki CalculateTotalPrice metodu için test yazılır. Metodun olduğu ProductService'in bağımlı olduğu Repository nesnesi normal şartlarda ürün listesini bir veritabanından veya bir servisten çekiyor olabilir. Test ortamı yazılırken bu kaynaklara erişim söz konusu olmaz, dolayısıyla bir Mock nesne ile sanki ürün listesi çekilmiş gibi ilerlemek gerekir zira amaç CalculateTotalPrice metodunun belli bir ürün listesi için doğru cevap dönüp dönmediğinin kanıtlanmasıdır.