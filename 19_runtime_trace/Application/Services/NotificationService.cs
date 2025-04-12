using Application.Model;

namespace Application.Services;

internal class NotificationService
{
    internal void Send(Customer customer, byte[] invoice)
    {
        Console.WriteLine("Sending invoice to customer...");
        Console.WriteLine($"Sending invoice to {customer.Fullname} at {customer.Email}");
        Console.WriteLine($"Invoice size: {invoice.Length} bytes");
    }
}
