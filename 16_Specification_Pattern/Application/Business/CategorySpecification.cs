using Application.Entity;
using Application.Contracts;

namespace Application.Business;

public class CategorySpecification : Specification<Product>
{
    private readonly string _category;

    public CategorySpecification(string category)
    {
        _category = category;
    }

    public override bool IsSatisfiedBy(Product product)
    {
        return product.Category == _category;
    }
}