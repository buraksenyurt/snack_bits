using Application.Contracts;
using Application.Business;
using Application.Entity;
using Application.Data;

namespace Application;

internal class Program 
{
    public static void Main()
    {
        var products = Repository.GetProducts();

        var categorySpec = new CategorySpecification("Electronics");
        var priceSpec = new PriceSpecification(300, 1000);
        var inStockSpec = new InStockSpecification();

        var finalSpec = categorySpec.And(priceSpec).And(inStockSpec);

        var filteredProducts = products.Where(p => finalSpec.IsSatisfiedBy(p)).ToList();

        foreach (var product in filteredProducts)
        {
            Console.WriteLine($"{product.Title} - {product.ListPrice} - {product.Category}");
        }
    }
}