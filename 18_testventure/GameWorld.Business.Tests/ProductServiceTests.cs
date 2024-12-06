
using Moq;

namespace GameWorld.Business.Tests;

public class ProductServiceTests
{
    [Fact]
    public void CalculateTotalPrice_Should_Return_Sum_Of_Product_Prices()
    {
        // Arange
        var mockRepository = new MockProductRepository();
        var service = new ProductService(mockRepository);

        // Act
        var totalPrice = service.GetTotalPrice();

        // Assert
        // Assert.Equal(0.0M,totalPrice);
        Assert.Equal(2055.69M, totalPrice);
    }

    // #01 Bu ikinci sürümde ise Moq paketinden yararlanılarak Mocking işlemi icra edilir

    [Fact]
    public void CalculateTotalPrice_Should_Return_Sum_Of_Product_Prices_With_Mock_Framework()
    {
        // Arange
        var mockRepository = new Mock<IProductRepository>();
        var products = new List<Product>()
        {
            new() {Id=1,Title="6ltı Bardak Takımı",Price=10.20M },
            new() {Id=2,Title="Kablosuz Kulaklık",Price=1999.99M },
            new() {Id=3,Title="Programming Rust Kitabı",Price=45.50M },
        };
        mockRepository.Setup(r => r.GetAll()).Returns(products);

        var service = new ProductService(mockRepository.Object);

        // Act
        var totalPrice = service.GetTotalPrice();

        // Assert
        Assert.Equal(2055.69M, totalPrice);
    }
}

// #00 Versiyonunda kendi Mock nesnemizi oluşturalım
class MockProductRepository : IProductRepository
{
    public IEnumerable<Product> GetAll()
    {
        return
        [
            new Product() {Id=1,Title="6ltı Bardak Takımı",Price=10.20M },
            new Product() {Id=2,Title="Kablosuz Kulaklık",Price=1999.99M },
            new Product() {Id=3,Title="Programming Rust Kitabı",Price=45.50M },
        ];
    }
}