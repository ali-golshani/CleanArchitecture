using Microsoft.Extensions.DependencyInjection;

namespace Framework.Mediator.DomainEvents;

internal sealed class DomainEventPublisher : IDomainEventPublisher
{
    private readonly IServiceProvider serviceProvider;

    public DomainEventPublisher(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public Task<Result<Empty>> Publish<TEvent>(TEvent @event, CancellationToken cancellationToken)
        where TEvent : IDomainEvent
    {
        return
            serviceProvider
            .GetRequiredService<DomainEventPublisher<TEvent>>()
            .Publish(@event, cancellationToken);
    }
}