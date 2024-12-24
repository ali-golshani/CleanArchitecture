namespace Framework.Specification;

public interface ISpecification<in T>
{
    bool IsSatisfiedBy(T value);
}
