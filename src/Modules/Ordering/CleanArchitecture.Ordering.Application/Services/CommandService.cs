using CleanArchitecture.Actors;
using Framework.Mediator.Extensions;
using Framework.Results;

namespace CleanArchitecture.Ordering.Application.Services;

internal sealed class CommandService(ActorPreservingScopeFactory scopeFactory) : ICommandService
{
    public async Task<Result<TResponse>> Handle<TRequest, TResponse>(ICommand<TRequest, TResponse> command, CancellationToken cancellationToken)
        where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        using var scope = scopeFactory.CreateScope();
        return await scope.ServiceProvider.SendByMediator(command, cancellationToken);
    }

    public async Task<Result<TResponse>> Handle<TRequest, TResponse>(Actor actor, ICommand<TRequest, TResponse> command, CancellationToken cancellationToken)
        where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        using var scope = scopeFactory.CreateScope(actor);
        return await scope.ServiceProvider.SendByMediator(command, cancellationToken);
    }
}
