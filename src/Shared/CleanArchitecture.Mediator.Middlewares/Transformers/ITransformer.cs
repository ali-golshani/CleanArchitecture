using CleanArchitecture.Actors;

namespace CleanArchitecture.Mediator.Middlewares.Transformers;

public interface ITransformer<T>
{
    int Order { get; }
    ValueTask<Result<T>> Transform(T value, Actor actor, CancellationToken cancellationToken);
}