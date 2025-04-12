using Application.Model;
using Application.Trace;

namespace Application.Services;

internal class NotificationService
{
    [MethodTrace]
    internal void Send(Customer customer, byte[] invoice)
    {
        Console.WriteLine("Sending invoice to customer...");
        Console.WriteLine($"Sending invoice to {customer.Fullname} at {customer.Email}");
        Console.WriteLine($"Invoice size: {invoice.Length} bytes");
    }
}
