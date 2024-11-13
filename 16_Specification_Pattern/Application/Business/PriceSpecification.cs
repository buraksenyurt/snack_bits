using Application.Contracts;
using Application.Entity;

namespace Application.Business;

public class PriceSpecification : Specification<Product>
{
    private readonly decimal _minPrice;
    private readonly decimal _maxPrice;

    public PriceSpecification(decimal minPrice, decimal maxPrice)
    {
        _minPrice = minPrice;
        _maxPrice = maxPrice;
    }

    public override bool IsSatisfiedBy(Product product)
    {
        return product.ListPrice >= _minPrice && product.ListPrice <= _maxPrice;
    }
}
