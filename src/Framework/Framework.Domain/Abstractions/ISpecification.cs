namespace Framework.Domain.Abstractions;

public interface ISpecification<in T>
{
    bool IsSatisfiedBy(T element);
}
