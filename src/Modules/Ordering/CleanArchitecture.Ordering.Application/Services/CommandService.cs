using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Application.Pipelines;
using Framework.Application.Extensions;
using Framework.Mediator.Extensions;
using Framework.Results;

namespace CleanArchitecture.Ordering.Application.Services;

internal sealed class CommandService(ContextPreservingScopeFactory scopeFactory) : ICommandService
{
    public Task<Result<TResponse>> Handle<TRequest, TResponse>(ICommand<TRequest, TResponse> command, CancellationToken cancellationToken)
        where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        var scope = scopeFactory.CreateScope();
        return Handle(scope.ServiceProvider, command, cancellationToken);
    }

    public Task<Result<TResponse>> Handle<TRequest, TResponse>(Actor actor, ICommand<TRequest, TResponse> command, CancellationToken cancellationToken)
        where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        var scope = scopeFactory.CreateScope(actor);
        return Handle(scope.ServiceProvider, command, cancellationToken);
    }

    private static Task<Result<TResponse>> Handle<TRequest, TResponse>(
        IServiceProvider serviceProvider,
        ICommand<TRequest, TResponse> command,
        CancellationToken cancellationToken)
        where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        serviceProvider.SetRequestContextAccessor(command.AsRequestType());
        return serviceProvider.SendToPipeline<TRequest, TResponse, CommandPipeline.Pipeline<TRequest, TResponse>>(command, cancellationToken);
    }
}
