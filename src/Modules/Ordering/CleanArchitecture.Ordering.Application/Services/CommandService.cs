using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Application.Pipelines;
using Framework.Mediator.Extensions;
using Framework.Results;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Application.Services;

internal class CommandService(IServiceProvider serviceProvider) : ICommandService
{
    public Task<Result<TResponse>> Handle<TRequest, TResponse>(ICommand<TRequest, TResponse> command, CancellationToken cancellationToken)
        where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        var pipeline = serviceProvider.GetRequiredService<CommandPipeline<TRequest, TResponse>>();
        return pipeline.Handle(command.AsRequestType(), cancellationToken);
    }

    public Task<Result<TResponse>> Handle<TRequest, TResponse>(Actor actor, ICommand<TRequest, TResponse> command, CancellationToken cancellationToken)
        where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        serviceProvider.ResolveActor(actor);
        return Handle(command, cancellationToken);
    }
}
