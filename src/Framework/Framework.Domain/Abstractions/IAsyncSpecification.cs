namespace Framework.Domain.Abstractions;

public interface IAsyncSpecification<in T>
{
    ValueTask<bool> IsSatisfiedBy(T element);
}
