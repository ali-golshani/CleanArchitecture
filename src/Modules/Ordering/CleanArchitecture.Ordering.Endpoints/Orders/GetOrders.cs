using CleanArchitecture.Authorization.WebApi.Policies.Permissions;
using Framework.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Ordering.Endpoints.Orders;

public sealed class GetOrders : IMinimalEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app
            .MapGet("orders", Handle)
            .WithTags("Orders")
            .WithDescription("Search Orders")
            .RequireAuthorization(new PermissionAuthorizeAttribute(Permission.ReadOrders))
            ;
    }

    private static
        Task<Results<Ok<PaginatedItems<Order>>, ProblemHttpResult>>
        Handle(IQueryService queryService, [AsParameters] Queries.Orders.GetOrders.Query query, CancellationToken cancellationToken)
    {
        return
            queryService
            .Handle(query, cancellationToken)
            .ToOkOrProblem();
    }
}
