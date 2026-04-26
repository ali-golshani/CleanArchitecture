namespace Framework.WebApi;

public interface IModule
{
    ModuleDocument Document { get; }
    string RoutePrefix { get; }

    void RegisterEndpoints(IEndpointRouteBuilder app);

    IEndpointRouteBuilder RouteBuilder(IEndpointRouteBuilder app)
    {
        return app.MapGroup(RoutePrefix).WithGroupName(Document.Name);
    }
}
