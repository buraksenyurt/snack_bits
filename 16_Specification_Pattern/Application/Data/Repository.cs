using Application.Entity;

namespace Application.Data;

public static class Repository
{
    public static List<Product> GetProducts()
    {
        return new List<Product>
        {
            new Product { Title = "Azon Laptop Entel i9 CPU", ListPrice = 4999.99M, Category = "Electronics", StockQuantity = 4 },
            new Product { Title = "Mirmana Hay Volume Bluetooth Kulaklık", ListPrice = 6999.50M, Category = "Electronics", StockQuantity = 0 },
            new Product { Title = "Super Speed Battery Charger", ListPrice = 55.55M, Category = "Inventory", StockQuantity = 5 },
            new Product { Title = "Ultra Thin TV 55 Inches", ListPrice = 7999.99M, Category = "Electronics", StockQuantity = 2 },
            new Product { Title = "Portable Speaker Pro", ListPrice = 149.99M, Category = "Electronics", StockQuantity = 10 },
            new Product { Title = "Smart Watch Series X", ListPrice = 299.99M, Category = "Wearables", StockQuantity = 7 },
            new Product { Title = "Wireless Keyboard and Mouse Combo", ListPrice = 79.99M, Category = "Electronics", StockQuantity = 15 },
            new Product { Title = "Gaming Console Z", ListPrice = 399.99M, Category = "Electronics", StockQuantity = 6 },
            new Product { Title = "4K Ultra HD Projector", ListPrice = 999.99M, Category = "Electronics", StockQuantity = 3 },
            new Product { Title = "Advanced Gaming Headset", ListPrice = 89.99M, Category = "Electronics", StockQuantity = 12 },
            new Product { Title = "Fitness Band Pro", ListPrice = 99.99M, Category = "Wearables", StockQuantity = 20 },
            new Product { Title = "Instant Pot Pressure Cooker", ListPrice = 129.99M, Category = "Home Appliances", StockQuantity = 8 },
            new Product { Title = "Cordless Drill Set", ListPrice = 79.99M, Category = "Tools", StockQuantity = 13 },
            new Product { Title = "Smart Refrigerator", ListPrice = 2499.99M, Category = "Home Appliances", StockQuantity = 1 },
            new Product { Title = "Bluetooth Portable Earpiece", ListPrice = 39.99M, Category = "Electronics", StockQuantity = 25 },
            new Product { Title = "High-Performance Vacuum Cleaner", ListPrice = 199.99M, Category = "Home Appliances", StockQuantity = 4 },
            new Product { Title = "Electric Kettle", ListPrice = 29.99M, Category = "Home Appliances", StockQuantity = 18 },
            new Product { Title = "Compact Digital Camera", ListPrice = 299.99M, Category = "Electronics", StockQuantity = 7 },
            new Product { Title = "Digital Thermometer", ListPrice = 15.99M, Category = "Health", StockQuantity = 50 },
            new Product { Title = "Professional Blender", ListPrice = 99.99M, Category = "Home Appliances", StockQuantity = 10 },
            new Product { Title = "Noise-Canceling Earbuds", ListPrice = 129.99M, Category = "Electronics", StockQuantity = 0 },
            new Product { Title = "Air Fryer Max", ListPrice = 149.99M, Category = "Home Appliances", StockQuantity = 5 },
            new Product { Title = "Electric Toothbrush Set", ListPrice = 79.99M, Category = "Health", StockQuantity = 12 },
            new Product { Title = "Smart Home Assistant", ListPrice = 89.99M, Category = "Electronics", StockQuantity = 22 },
            new Product { Title = "Ergonomic Office Chair", ListPrice = 199.99M, Category = "Furniture", StockQuantity = 3 },
        };
    }
}
