namespace CleanArchitecture.Shared;

public interface ISpecification<in T>
{
    bool IsSatisfiedBy(T element);
}
