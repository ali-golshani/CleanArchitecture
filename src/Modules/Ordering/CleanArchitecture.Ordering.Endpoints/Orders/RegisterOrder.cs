using CleanArchitecture.Authorization.Claims;
using CleanArchitecture.Authorization.WebApi.Policies.Permissions;
using CleanArchitecture.Ordering.Commands;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Ordering.Endpoints.Orders;

public sealed class RegisterOrder : IMinimalEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app
            .MapPost("orders", Handle)
            .WithTags("Orders")
            .WithDescription("Register Order")
            .RequireAuthorization(new PermissionAuthorizeAttribute(Permission.RegisterOrder))
            ;
    }

    private static
        Task<Results<Ok, ProblemHttpResult>>
        Handle(ICommandService commandService, [FromBody] Commands.Orders.RegisterOrder.Command command, CancellationToken cancellationToken)
    {
        return
            commandService
            .Handle(command, cancellationToken)
            .ToOkOrProblem();
    }
}
