
using Application.Trace;

namespace Application.Business;

internal class ChartBusiness
{
    [MethodTrace]
    internal bool CheckStock(object products)
    {
        Console.WriteLine("Checking stock...");
        return true;
    }
}
