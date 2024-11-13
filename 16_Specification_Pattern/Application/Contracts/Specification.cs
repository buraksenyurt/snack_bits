namespace Application.Contracts;

public abstract class Specification<T> : ISpecification<T>
{
    public abstract bool IsSatisfiedBy(T item);

    public ISpecification<T> And(ISpecification<T> other)
    {
        return new AndSpecification<T>(this, other);
    }
    public ISpecification<T> Or(ISpecification<T> other)
    {
        return new OrSpecification<T>(this, other);
    }
    public ISpecification<T> Not()
    {
        return new NotSpecification<T>(this);
    }
}
