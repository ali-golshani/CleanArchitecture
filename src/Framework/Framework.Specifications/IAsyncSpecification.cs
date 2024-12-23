namespace Framework.Specifications;

public interface IAsyncSpecification<in T>
{
    ValueTask<bool> IsSatisfiedBy(T element);
}
