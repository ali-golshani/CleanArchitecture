namespace Framework.WebApi.Extensions;

public static class EndpointExtensions
{
    public static void Map<T>(this IEndpointRouteBuilder app)
        where T : IMinimalEndpoint
    {
        T.AddRoute(app);
    }

    public static void RegisterModule(this IEndpointRouteBuilder app, IModule module)
    {
        var route = app.MapGroup(module.RoutePrefix).WithGroupName(module.Name);
        module.RegisterEndpoints(route);
    }
}
