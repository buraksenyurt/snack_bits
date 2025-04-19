using Application.Business;
using Application.Model;
using Application.Services;
using Application.Trace;
using Application.View;
using Serilog;

namespace Application;

/*
    Örnekteki amaç çalışma zamanında kod geçişlerini izleyerek loglamak. 
    Bu nedenle OOP prensipleri, DI mekanizmaları ele alınmayıp teknik borç kabul edilmiştir.
*/

internal class Program
{
    static void Main()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        MethodTraceAttribute.Logger = new SerilogAdapter();

        Log.Information("Application started");

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

        var shoppingPage = new ShoppingPage(
                new OrderService(
                    new CustomerBusiness()
                    , new OrderBusiness()
                    , new ChartBusiness()
                    , new PaymentBusiness()
                    ),
                new InvoiceService(),
                new NotificationService()
            );
        ShoppingPage.Load();
        var orderResult = shoppingPage.Order(order, chart); // Bu metot çağrımının çalışma zamanındaki işleyişini izlemek istiyoruz (Call Stack Tracing)
        Console.WriteLine(orderResult.IsSuccess);

        Log.CloseAndFlush();
    }
}
