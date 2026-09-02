using System.Data.Common;

namespace IntegrationEventBus.Abstractions;

/// <summary>
/// Persists integration events for durable, asynchronous delivery.
/// </summary>
public interface IIntegrationEventPublisher
{
    /// <summary>
    /// Persists an integration event and its deliveries using the supplied business transaction.
    /// </summary>
    /// <typeparam name="TEvent">The CLR type of the integration event.</typeparam>
    /// <param name="integrationEvent">The event payload.</param>
    /// <param name="transaction">
    /// The active business transaction. The event becomes visible to processors only if this
    /// transaction is committed.
    /// </param>
    /// <param name="options">Optional event metadata.</param>
    /// <param name="cancellationToken">A token used to cancel the database operation.</param>
    /// <returns>
    /// The stable event identifier. Returning an identifier does not imply that the transaction
    /// has been committed.
    /// </returns>
    ValueTask<Guid> PublishAsync<TEvent>(
        TEvent integrationEvent,
        DbTransaction transaction,
        PublishOptions? options = null,
        CancellationToken cancellationToken = default)
        where TEvent : notnull;
}
