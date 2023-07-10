# Çok Fazla If Bloğu Yerine Rule Engine Kullanmak

Kod kalite metriklerini artırmak için yaptığımız çalışmalardan birisi de fonksiyonların Cognitive Complexity değerlerini azaltmak için if, switch gibi yapıları strateji deseni kullanarak ortadan kaldırmak. Ancak bazen öyle bloklara denk gelebiliyoruz ki, olay belirli koşullar gerçekleştiğinde bir değer döndürmek üzerine dayalı oluyor. Mesela bir siparişte iskonto oranını belirlemek için state nesnelerinin belli koşulları sağlayıp sağlamadığına bakmak gibi. Bu tip kurgular kod tabanı içerisinde çok fazla olduğunda belki bir kural motoru ile yönetmek düşünülebilir. Bunun over-engineering olduğunu da göz ardı etmemek lazım. Ama bir deneyip görmekte yarar var. Merak işte :D Aşağıdaki program kodunu ele alalım.

```csharp
using Domain;

public class Program
{
    static void Main()
    {
        var monitor = new Product
        {
            Id = 1,
            Title = "Widescreen 42 inch Full HD Curve monitor",
            ListPrice = 1000M,
            StockLevel = 12
        };

        var orders = new List<Order>
            {
                new Order
                {
                    Id = 1001,
                    Date = DateTime.Now,
                    CustomerType = CustomerType.AuthorizedDealer,
                    ProductId = monitor.Id,
                    Quantity = 2
                },
                new Order
                {
                    Id = 1002,
                    Date = DateTime.Now,
                    CustomerType = CustomerType.Individual,
                    ProductId = monitor.Id,
                    Quantity = 2
                },
                new Order
                {
                    Id = 1003,
                    Date = DateTime.Now,
                    CustomerType = CustomerType.GroupFirm,
                    ProductId = monitor.Id,
                    Quantity = 3
                },
                new Order
                {
                    Id = 1004,
                    Date = DateTime.Now,
                    CustomerType = CustomerType.Distributor,
                    ProductId = monitor.Id,
                    Quantity = 1
                },
                new Order
                {
                    Id = 1005,
                    Date = DateTime.Now,
                    CustomerType = CustomerType.Seller,
                    ProductId = monitor.Id,
                    Quantity = 1
                }
            };

        foreach (var order in orders)
        {
            if (order.Quantity < monitor.StockLevel)
            {
                int discountRate;
                switch (order.CustomerType)
                {
                    case CustomerType.AuthorizedDealer:
                        discountRate = 2;
                        break;
                    case CustomerType.Distributor:
                        discountRate = 5;
                        break;
                    case CustomerType.Individual:
                        discountRate = 3;
                        break;
                    case CustomerType.GroupFirm:
                        discountRate = 18;
                        break;
                    default:
                        discountRate = 0;
                        break;
                }
                Console.WriteLine($"İndirim tutarı % {discountRate}");
            }
        }
    }
}

namespace Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public decimal ListPrice { get; set; }
        public int StockLevel { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public CustomerType CustomerType { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public enum CustomerType
    {
        AuthorizedDealer,
        Distributor,
        Individual,
        GroupFirm,
        Seller
    }
}
```

Örnek kod parçasında bir ürünle ilgili birkaç tane sipariş nesnesi yer alıyor. Siparişi veren müşteri tipine ve stok durumuna göre de iskonto oranı belirleniyor. Bunun için çok doğal olarak sadece if...else kullanmamız yeterli ve bundan hiçbir sorun yok.