using CleanArchitecture.Authorization.Claims;
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

public sealed class RegisterAndApproveOrder : IMinimalEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app
            .MapPost("orders/register-and-approve", Handle)
            .WithTags("Orders")
            .WithDescription("Register and Approve Order")
            .RequireAuthorization(new PermissionAuthorizeAttribute(Permission.RegisterOrder))
            ;
    }

    private static async Task<Results<Ok, ProblemHttpResult>> Handle(
        IService registerService,
        [FromBody] Request request,
        CancellationToken cancellationToken)
    {
        return await
            registerService
            .Handle(request, cancellationToken)
            .ToOkOrProblem();
    }
}
