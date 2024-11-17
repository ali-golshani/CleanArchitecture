using CleanArchitecture.Actors;
using Framework.Results;

namespace CleanArchitecture.Ordering.Commands;

public interface ICommandService
{
    Task<Result<TResponse>> Handle<TRequest, TResponse>(ICommand<TRequest, TResponse> command, CancellationToken cancellationToken) where TRequest : CommandBase, ICommand<TRequest, TResponse>;
    Task<Result<TResponse>> Handle<TRequest, TResponse>(Actor actor, ICommand<TRequest, TResponse> command, CancellationToken cancellationToken) where TRequest : CommandBase, ICommand<TRequest, TResponse>;
}