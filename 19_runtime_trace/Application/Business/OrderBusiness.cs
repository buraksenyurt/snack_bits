using Application.Model;
using Application.Trace;

namespace Application.Business;

internal class OrderBusiness
{
    [MethodTrace]
    internal bool Complete(Order order, ShoppingCart chart)
    {
        Console.WriteLine("Completing order...");
        Console.WriteLine($"Order for {order.Customer.Fullname} with {chart.GetTotalPrice()} total.");
        Console.WriteLine($"Payment type: {order.PaymentType}");

        return true;
    }
}
