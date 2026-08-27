using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Mediator.Middlewares.Extensions;

public static class CorrelationIdExtensions
{
    public static void UseCorrelationId(this IServiceProvider serviceProvider, Guid correlationId)
    {
        serviceProvider.GetRequiredService<CorrelationIdAccessor>().Initialize(correlationId);
    }
}
