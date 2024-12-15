using CleanArchitecture.Actors;

namespace CleanArchitecture.Mediator.Middlewares.Transformers;

public abstract class EnrichingTransformer<T> : ITransformer<T>
{
    protected abstract ValueTask<Result<T>> Enrich(T value, CancellationToken cancellationToken);

    public abstract int Order { get; }

    public async ValueTask<Result<T>> Transform(T value, Actor actor, CancellationToken cancellationToken)
    {
        return await Enrich(value, cancellationToken);
    }
}
