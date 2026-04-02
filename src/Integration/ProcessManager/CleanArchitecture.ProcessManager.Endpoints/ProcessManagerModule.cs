using Framework.WebApi;
using Framework.WebApi.Extensions;
using Microsoft.AspNetCore.Routing;

namespace CleanArchitecture.ProcessManager.Endpoints;

public sealed class ProcessManagerModule : IModule
{
    public string Name => "ProcessManager";
    public string Title => "Process Manager";
    public string Version => "1.0.0";
    public string RoutePrefix => "api/process-manager/";

    public void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.Map<Orders.RegisterAndApproveOrder>();
        app.Map<Orders.ScheduleRegisterAndApproveOrder>();
    }
}
