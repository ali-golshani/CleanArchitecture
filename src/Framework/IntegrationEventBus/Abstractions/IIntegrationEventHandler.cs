namespace IntegrationEventBus.Abstractions;

/// <summary>
/// Handles one type of integration event for a configured subscription.
/// </summary>
/// <typeparam name="TEvent">The CLR type of the integration event.</typeparam>
/// <remarks>
/// A handler is executed with at-least-once semantics and must tolerate duplicate delivery.
/// Completion indicates success; an exception indicates failure and is handled by the configured
/// retry policy. No database transaction is supplied to the handler.
/// </remarks>
public interface IIntegrationEventHandler<in TEvent>
    where TEvent : notnull
{
    /// <summary>
    /// Processes an integration event.
    /// </summary>
    ValueTask HandleAsync(
        TEvent integrationEvent,
        IntegrationEventContext context,
        CancellationToken cancellationToken);
}
