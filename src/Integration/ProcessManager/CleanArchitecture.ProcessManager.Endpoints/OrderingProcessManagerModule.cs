using Framework.WebApi;
using Framework.WebApi.Extensions;
using Microsoft.AspNetCore.Routing;

namespace CleanArchitecture.ProcessManager.Endpoints;

public sealed class OrderingProcessManagerModule : IModule
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
        app.Map<Orders.RegisterAndApproveOrder>();
        app.Map<Orders.ScheduleRegisterAndApproveOrder>();
    }
}
