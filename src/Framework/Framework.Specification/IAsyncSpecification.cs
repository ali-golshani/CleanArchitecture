namespace Framework.Specification;

public interface IAsyncSpecification<in T>
{
    ValueTask<bool> IsSatisfiedBy(T element);
}
