using CleanArchitecture.Authorization.WebApi.Policies.Permissions;
using CleanArchitecture.ProcessManager.RegisterAndApproveOrder;
using Framework.WebApi;
using Framework.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CleanArchitecture.ProcessManager.Endpoints.Orders;

public sealed class ScheduleRegisterAndApproveOrder : IMinimalEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app
            .MapPost("orders/schedule/register-and-approve", Handle)
            .WithTags("Orders")
            .WithDescription("Schedule Register and Approve Order")
            .RequireAuthorization(new PermissionAuthorizeAttribute(Permission.RegisterOrder))
            ;
    }

    private static async Task<NoContent> Handle(
        ISchedulingService schedulingService,
        [FromBody] Request request)
    {
        return await
            schedulingService
            .Schedule(request)
            .ToNoContent();
    }
}
