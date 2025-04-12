namespace Application.Model;

internal class ShoppingCart(Customer customer)
{
    private readonly Customer _customer = customer;
    private readonly List<Product> items = [];

    internal void AddItem(Product product)
    {
        items.Add(product);
    }

    internal decimal GetTotalPrice()
    {
        return items.Sum(item => item.ListPrice);
    }

    internal Customer GetCustomer()
    {
        return _customer;
    }
}