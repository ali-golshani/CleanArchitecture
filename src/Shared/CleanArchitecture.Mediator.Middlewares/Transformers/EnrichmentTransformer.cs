using CleanArchitecture.Actors;

namespace CleanArchitecture.Mediator.Middlewares.Transformers;

public abstract class EnrichmentTransformer<T> : ITransformer<T>
{
    protected abstract ValueTask<Result<T>> Transform(T value, CancellationToken cancellationToken);

    public abstract int Order { get; }

    public async ValueTask<Result<T>> Transform(T value, Actor actor, CancellationToken cancellationToken)
    {
        return await Transform(value, cancellationToken);
    }
}
