using CleanArchitecture.Actors;
using CleanArchitecture.Actors.Extensions;
using CleanArchitecture.Mediator.Middlewares.Extensions;
using Framework.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class RequestExecutionScopeFactory(IServiceProvider serviceProvider)
{
    public RequestExecutionScope CreateScope(RequestExecutionOptions? options = null)
    {
        var actor = serviceProvider.GetRequiredService<IActorResolver>().Actor;
        return CreateScope(actor, options);
    }

    public RequestExecutionScope CreateScope(Actor? actor, RequestExecutionOptions? options = null)
    {
        var correlationId = 
            options?.CorrelationId
            ?? serviceProvider.GetRequiredService<ICorrelationIdAccessor>().CorrelationId
            ?? Guid.NewGuid();

        var scope = serviceProvider.CreateScope();

        if (actor is not null)
        {
            scope.ServiceProvider.UseActor(actor);
        }

        scope.ServiceProvider.UseCorrelationId(correlationId);

        return new RequestExecutionScope(scope, correlationId);
    }
}
