namespace Framework.Specification;

public abstract class Specification<T> : ISpecification<T>
{
    public static Specification<T> operator &(Specification<T> left, ISpecification<T> right)
    {
        return new AndSpecification<T>(left, right);
    }

    public static Specification<T> operator |(Specification<T> left, ISpecification<T> right)
    {
        return new OrSpecification<T>(left, right);
    }

    public static Specification<T> operator ^(Specification<T> left, ISpecification<T> right)
    {
        return new XOrSpecification<T>(left, right);
    }

    public abstract bool IsSatisfiedBy(T value);
}
