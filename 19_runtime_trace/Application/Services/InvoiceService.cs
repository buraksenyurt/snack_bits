using Application.Model;
using Application.Trace;

namespace Application.Services;

internal class InvoiceService
{
    [MethodTrace]
    internal byte[] Create(Order order, ShoppingCart chart)
    {
        Console.WriteLine("Creating invoice...");
        Console.WriteLine($"Invoice for {order.Customer.Fullname} with {chart.GetTotalPrice()} total.");
        Console.WriteLine($"Payment type: {order.PaymentType}");
        Console.WriteLine($"Invoice created successfully!");

        return [];
    }
}
