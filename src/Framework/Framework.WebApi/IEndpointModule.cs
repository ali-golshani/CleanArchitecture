using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Framework.WebApi;

public interface IEndpointModule
{
    ModuleDocument Document { get; }
    string RoutePrefix { get; }

    void RegisterEndpoints(IEndpointRouteBuilder app);

    IEndpointRouteBuilder RouteBuilder(IEndpointRouteBuilder app)
    {
        return app.MapGroup(RoutePrefix).WithGroupName(Document.Name);
    }
}
