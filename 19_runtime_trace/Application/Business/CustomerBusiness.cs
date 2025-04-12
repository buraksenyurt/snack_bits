using Application.Model;
using Application.Trace;

namespace Application.Business;

internal class CustomerBusiness
{
    [MethodTrace]
    internal bool ValidateCustomer(Customer customer)
    {
        Console.WriteLine("Validating customer...");
        return true;
    }
}