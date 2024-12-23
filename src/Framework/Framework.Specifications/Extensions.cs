namespace Framework.Specifications;

public static class Extensions
{
    public static Specification<T> And<T>(this ISpecification<T> left, ISpecification<T> right)
    {
        return new AndSpecification<T>(left, right);
    }

    public static Specification<T> Or<T>(this ISpecification<T> left, ISpecification<T> right)
    {
        return new OrSpecification<T>(left, right);
    }

    public static Specification<T> XOr<T>(this ISpecification<T> left, ISpecification<T> right)
    {
        return new XOrSpecification<T>(left, right);
    }
}
