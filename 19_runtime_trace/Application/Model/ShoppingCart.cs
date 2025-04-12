namespace Application.Model;

internal class ShoppingCart(Customer customer)
{
    private Customer customer = customer;
    private List<Product> items = [];

    internal void AddItem(Product product)
    {
        items.Add(product);
    }
}