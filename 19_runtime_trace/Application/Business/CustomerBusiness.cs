using Application.Model;

namespace Application.Business;

internal class CustomerBusiness
{
    internal bool ValidateCustomer(Customer customer)
    {
        Console.WriteLine("Validating customer...");
        return true;
    }
}