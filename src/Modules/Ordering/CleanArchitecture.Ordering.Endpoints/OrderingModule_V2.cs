namespace CleanArchitecture.Ordering.Endpoints;

public sealed class OrderingModule_V2 : IModule
{
    public string Name => "Ordering_V2";
    public string Title => "Ordering";
    public string Version => "2.0.0";
    public string RoutePrefix => "api/v2/ordering/";

    public void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.Map<V2.Orders.GetOrder>();
    }
}
