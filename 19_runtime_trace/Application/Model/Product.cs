namespace Application.Model;

internal class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Category Category { get; set; }
    public decimal ListPrice  { get; set; }
}
