namespace Framework.WebApi.Extensions;

public static class EndpointExtensions
{
    public static void Map<T>(this IEndpointRouteBuilder app)
        where T : IMinimalEndpoint
    {
        T.AddRoute(app);
    }

    public static void RegisterEndpointModule(this IEndpointRouteBuilder app, IEndpointModule module)
    {
        var route = module.RouteBuilder(app);
        module.RegisterEndpoints(route);
    }
}
