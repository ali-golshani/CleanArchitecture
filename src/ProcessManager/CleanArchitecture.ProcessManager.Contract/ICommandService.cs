using CleanArchitecture.Actors;
using Framework.Results;

namespace CleanArchitecture.ProcessManager;

public interface ICommandService
{
    Task<Result<TResponse>> Handle<TRequest, TResponse>(ICommand<TRequest, TResponse> command, CancellationToken cancellationToken) where TRequest : CommandBase, ICommand<TRequest, TResponse>;
}