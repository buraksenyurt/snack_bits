using Application.Model;
using Application.Services;

namespace Application.View;

internal class ShoppingPage(OrderService orderService)
{
    private readonly OrderService _orderService = orderService;

    public void Load()
    {
        Console.WriteLine("Loading shopping page...");
    }

    public Result Order()
    {
        var vanDam = new Customer
        {
            Fullname = "Jean-Claude Van Dam",
            Email = "cani@jcvd.com"
        };

        var someMovie = new Product
        {
            Name = "Bloodsport",
            ListPrice = 9.99m,
            Category = new Category
            {
                Title = "Action Movie"
            }
        };

        var someBook = new Product
        {
            Name = "Tsin Zu - Di Art of War",
            ListPrice = 19.99m,
            Category = new Category
            {
                Title = "Book"
            }
        };

        var chart = new ShoppingCart(vanDam);
        chart.AddItem(someMovie);
        chart.AddItem(someBook);

        var paymentInfo = new CreditCardPayment
        {
            CardNumber = "1234-5678-9012-3456",
            ExpirationDate = DateTime.Now.AddYears(1),
            Cvv = "123"
        };

        var order = new Order
        {
            Customer = vanDam,
            PaymentType = PaymentType.CreditCard,
            PaymentInfo = paymentInfo
        };

        var result = _orderService.CreateOrder(order, chart);
        if (result.IsSuccess)
        {
            Console.WriteLine("Order created successfully!");
            
        }
        else
        {
            Console.WriteLine($"Error: {result.ErrorMessage}");
        }
        return result;
    }
}
