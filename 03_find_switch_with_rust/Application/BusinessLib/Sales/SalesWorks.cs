using BusinessLib.Constant;
using BusinessLib.Data;

namespace BusinessLib.Sales;

public class SalesWorks
{
    public decimal FindDiscount(Customer customer)
    {
        decimal discount = 1.1M;
        switch (customer.CustomerType)
        {
            case CustomerType.Standard:
                discount = 2.5M;
                break;
            case CustomerType.Gold:
                discount = 3.5M;
                break;
            case CustomerType.Platinium:
                discount = 4.0M;
                break;
            case CustomerType.Veteran:
                discount = 1.28M;
                break;
            case CustomerType.YouthPlus:
                discount = 1.02M;
                break;
            case CustomerType.Student:
                discount = 1.05M;
                break;
        }
        return discount;
    }
}
