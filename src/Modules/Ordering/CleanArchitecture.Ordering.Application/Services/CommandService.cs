using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Application.Pipelines;
using Framework.Application.Extensions;
using Framework.Mediator.Extensions;
using Framework.Results;

namespace CleanArchitecture.Ordering.Application.Services;

internal class CommandService(IServiceProvider serviceProvider) : ICommandService
{
    public Task<Result<TResponse>> Handle<TRequest, TResponse>(ICommand<TRequest, TResponse> command, CancellationToken cancellationToken)
        where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        var scope = serviceProvider.CreateScopeWithPreservedActor();
        var scopedServiceProvider = scope.ServiceProvider;

        scopedServiceProvider.SetRequestContextAccessor(command.AsRequestType());
        return scopedServiceProvider.SendToPipeline<TRequest, TResponse, CommandPipeline.Pipeline<TRequest, TResponse>>(command, cancellationToken);
    }

    public Task<Result<TResponse>> Handle<TRequest, TResponse>(Actor actor, ICommand<TRequest, TResponse> command, CancellationToken cancellationToken)
        where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        serviceProvider.ResolveActor(actor);
        return Handle(command, cancellationToken);
    }
}
