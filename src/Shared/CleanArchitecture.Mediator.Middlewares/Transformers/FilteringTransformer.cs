using CleanArchitecture.Actors;

namespace CleanArchitecture.Mediator.Middlewares.Transformers;

public abstract class FilteringTransformer<T> : ITransformer<T>
{
    protected abstract T Filter(T value, Actor actor);

    public abstract int Order { get; }

    public ValueTask<Result<T>> Transform(T value, Actor actor, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(Result<T>.Success(Filter(value, actor)));
    }
}
