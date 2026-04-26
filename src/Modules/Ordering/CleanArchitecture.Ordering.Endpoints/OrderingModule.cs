namespace CleanArchitecture.Ordering.Endpoints;

public sealed class OrderingModule : IModule
{
    public ModuleDocument Document { get; } = new()
    {
        Name = "Ordering",
        Title = "Ordering",
        Version = "1.0.0",
    };

    public string RoutePrefix => "api/ordering/";

    public void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.Map<Orders.GetOrders>();
        app.Map<Orders.GetOrder>();
        app.Map<Orders.RegisterOrder>();
    }
}
