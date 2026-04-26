using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Application.Pipelines;
using Framework.Mediator.Extensions;
using Framework.Results;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Application.Services;

internal sealed class CommandService(ActorPreservingScopeFactory scopeFactory) : ICommandService
{
    public async Task<Result<TResponse>> Handle<TRequest, TResponse>(ICommand<TRequest, TResponse> command, CancellationToken cancellationToken)
        where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        using var scope = scopeFactory.CreateScope();
        var pipeline = scope.ServiceProvider.GetRequiredService<CommandPipeline.Pipeline<TRequest, TResponse>>();
        return await pipeline.Handle(command.AsRequestType(), cancellationToken);
    }

    public async Task<Result<TResponse>> Handle<TRequest, TResponse>(Actor actor, ICommand<TRequest, TResponse> command, CancellationToken cancellationToken)
        where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        using var scope = scopeFactory.CreateScope(actor);
        var pipeline = scope.ServiceProvider.GetRequiredService<CommandPipeline.Pipeline<TRequest, TResponse>>();
        return await pipeline.Handle(command.AsRequestType(), cancellationToken);
    }
}
