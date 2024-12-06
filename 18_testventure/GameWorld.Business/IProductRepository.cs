namespace GameWorld.Business;

public interface IProductRepository
{
    IEnumerable<Product> GetAll();
}
