using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class RequestExecutionScope(IServiceScope scope, Guid correlationId) : IDisposable
{
    public IServiceProvider ServiceProvider => scope.ServiceProvider;
    public Guid CorrelationId { get; } = correlationId;

    public void Dispose() => scope.Dispose();
}
