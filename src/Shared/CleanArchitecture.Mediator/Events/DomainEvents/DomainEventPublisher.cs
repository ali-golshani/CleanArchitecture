using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Mediator;

internal sealed class DomainEventPublisher : IDomainEventPublisher
{
    private readonly IServiceProvider serviceProvider;

    public DomainEventPublisher(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public Task<Result<Empty>> Publish<TEvent>(TEvent @event)
        where TEvent : IDomainEvent
    {
        return
            serviceProvider
            .GetRequiredService<DomainEventPublisher<TEvent>>()
            .Publish(@event);
    }
}