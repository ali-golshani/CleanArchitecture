using Framework.Mediator.Requests;

namespace CleanArchitecture.WebApi.Shared.Swagger;

internal class RequestPropertiesFilter : ReadOnlyPropertiesFilter<Request>
{
    protected override string[] ReadOnlyProperties { get; } =
    [
        nameof(Request.CorrelationId),
        nameof(Request.RequestTitle),
        nameof(Request.RequestTime),
        nameof(Request.ShouldLog),
    ];
}