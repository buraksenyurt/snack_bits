using System;

namespace Application.Entity;

public class Product
{
    public string? Title { get; set; }
    public decimal ListPrice { get; set; }
    public string? Category { get; set; }
    public int StockQuantity { get; set; }
}
