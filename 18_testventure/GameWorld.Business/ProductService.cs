namespace GameWorld.Business;

public class ProductService
{
    private readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public decimal GetTotalPrice()
    {
        var products = _productRepository.GetAll();
        return products.Sum(p => p.Price);
    }
}
