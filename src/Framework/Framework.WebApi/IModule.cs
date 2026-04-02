namespace Framework.WebApi;

public interface IModule
{
    string Name { get; }
    string Title { get; }
    string Version { get; }
    string RoutePrefix { get; }

    void RegisterEndpoints(IEndpointRouteBuilder app);

    IEndpointRouteBuilder RouteBuilder(IEndpointRouteBuilder app)
    {
        return app.MapGroup(RoutePrefix).WithGroupName(Name);
    }
}
