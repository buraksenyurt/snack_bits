using Application.Services;
using Application.View;

namespace Application;

/*
    Örnekteki amaç çalışma zamanında kod geçişlerini izleyerek loglamak. 
    Bu nedenle OOP prensipleri, DI mekanizmaları ele alınmayıp teknik borç kabul edilmiştir.
*/

internal class Program
{
    static void Main()
    {
        var shoppingPage = new ShoppingPage(new OrderService());
        shoppingPage.Load();
        var orderResult = shoppingPage.Order();
        Console.WriteLine(orderResult.IsSuccess);

    }
}
