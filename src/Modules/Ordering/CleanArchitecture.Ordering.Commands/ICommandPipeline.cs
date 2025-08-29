using Framework.Mediator.Middlewares;

namespace CleanArchitecture.Ordering.Commands;

public interface ICommandPipeline<TRequest, TResponse> : IPipeline<TRequest, TResponse>
    where TRequest : CommandBase, ICommand<TRequest, TResponse>;