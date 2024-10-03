namespace CleanArchitecture.Shared;

public interface IAsyncSpecification<in T>
{
    ValueTask<bool> IsSatisfiedBy(T element);
}
