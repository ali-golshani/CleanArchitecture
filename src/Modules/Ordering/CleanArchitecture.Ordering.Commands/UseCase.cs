using CleanArchitecture.Actors;
using CleanArchitecture.Mediator;

namespace CleanArchitecture.Ordering.Commands;

public abstract class UseCase<TRequest, TResponse>(ActorPreservingScopeFactory scopeFactory)
    : UseCase<TRequest, TResponse, ICommandPipeline<TRequest, TResponse>>(scopeFactory)
    where TRequest : CommandBase, ICommand<TRequest, TResponse>;