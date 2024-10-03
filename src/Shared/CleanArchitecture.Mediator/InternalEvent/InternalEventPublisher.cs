using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Mediator;

internal sealed class InternalEventPublisher : IInternalEventPublisher
{
    private readonly IServiceProvider serviceProvider;

    public InternalEventPublisher(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public Task<Result<Empty>> Publish<TEvent>(TEvent @event)
        where TEvent : IInternalEvent
    {
        return
            serviceProvider
            .GetRequiredService<InternalEventPublisher<TEvent>>()
            .Publish(@event);
    }
}