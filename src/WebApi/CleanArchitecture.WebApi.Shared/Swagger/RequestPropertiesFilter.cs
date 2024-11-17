namespace CleanArchitecture.WebApi.Shared.Swagger;

internal class RequestPropertiesFilter : ReadOnlyPropertiesFilter<Mediator.Request>
{
    protected override string[] ReadOnlyProperties { get; } =
    [
        nameof(Mediator.Request.CorrelationId),
        nameof(Mediator.Request.RequestTitle),
        nameof(Mediator.Request.RequestTime),
        nameof(Mediator.Request.ShouldLog),
    ];
}