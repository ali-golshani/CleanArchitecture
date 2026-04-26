using Framework.Mediator;

namespace CleanArchitecture.WebApi.Shared.Swagger;

internal sealed class RequestReadOnlyPropertiesTransformer : ReadOnlyPropertiesTransformer<Request>
{
    protected override string[] ReadOnlyProperties { get; } =
    [
        nameof(Request.CorrelationId),
        nameof(Request.RequestTitle),
        nameof(Request.RequestTime),
        nameof(Request.ShouldLog),
        nameof(Request.LoggingDomain),
    ];
}