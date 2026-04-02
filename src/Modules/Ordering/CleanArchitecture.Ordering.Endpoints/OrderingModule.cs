namespace CleanArchitecture.Ordering.Endpoints;

public sealed class OrderingModule : IModule
{
    public string Name => "Ordering";
    public string Title => "Ordering";
    public string Version => "1.0.0";
    public string RoutePrefix => "api/ordering/";

    public void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.Map<Orders.GetOrders>();
        app.Map<Orders.GetOrder>();
        app.Map<Orders.RegisterOrder>();
    }
}
