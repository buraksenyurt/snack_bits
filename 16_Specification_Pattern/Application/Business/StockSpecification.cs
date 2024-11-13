using Application.Contracts;
using Application.Entity;

namespace Application.Business;

public class InStockSpecification : Specification<Product>
{
    public override bool IsSatisfiedBy(Product product)
    {
        return product.StockQuantity > 0;
    }
}
