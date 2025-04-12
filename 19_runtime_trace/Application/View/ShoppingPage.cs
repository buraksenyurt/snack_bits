using Application.Model;
using Application.Services;
using Application.Trace;

namespace Application.View;

internal class ShoppingPage(OrderService orderService, InvoiceService invoiceService, NotificationService noticationService)
{
    private readonly OrderService _orderService = orderService;
    private readonly InvoiceService _invoiceService = invoiceService;
    private readonly NotificationService _noticationService = noticationService;

    public static void Load()
    {
        Console.WriteLine("Loading shopping page...");
    }

    [MethodTrace]
    public Result Order(Order order, ShoppingCart chart)
    {
        var result = _orderService.CreateOrder(order, chart);
        if (result.IsSuccess)
        {
            Console.WriteLine("Order created successfully!");
            byte[] invoice = _invoiceService.Create(order, chart);
            _noticationService.Send(order.Customer, invoice);
        }
        else
        {
            Console.WriteLine($"Error: {result.ErrorMessage}");
        }

        return result;
    }
}
