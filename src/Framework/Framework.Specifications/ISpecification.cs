namespace Framework.Specifications;

public interface ISpecification<in T>
{
    bool IsSatisfiedBy(T value);
}
