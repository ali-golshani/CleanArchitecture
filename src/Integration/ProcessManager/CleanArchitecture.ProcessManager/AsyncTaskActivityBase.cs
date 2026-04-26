using CleanArchitecture.Actors;
using CleanArchitecture.Actors.Extensions;
using DurableTask.Core;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.ProcessManager;

public abstract class AsyncTaskActivityBase<TRequest, TResponse> : AsyncTaskActivity<TRequest, TResponse>
{
    protected abstract Task<TResponse> ExecuteAsync(IServiceProvider serviceProvider, TRequest input);

    protected readonly InternalServiceActor Actor;
    private readonly IServiceProvider serviceProvider;

    protected AsyncTaskActivityBase(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
        Actor = new InternalServiceActor(GetType().Name);
    }

    protected override async Task<TResponse> ExecuteAsync(TaskContext context, TRequest input)
    {
        using var scope = serviceProvider.CreateScope();
        scope.ServiceProvider.ResolveActor(Actor);
        return await ExecuteAsync(scope.ServiceProvider, input);
    }
}
