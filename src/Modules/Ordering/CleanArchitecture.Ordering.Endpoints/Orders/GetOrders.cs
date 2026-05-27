using CleanArchitecture.Authorization.Claims;
using CleanArchitecture.Authorization.WebApi.Policies.Permissions;
using Framework.Queries;

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
        async Task<Results<Ok<PaginatedItems<Order>>, ProblemHttpResult>>
        Handle(IQueryService queryService, [AsParameters] Queries.Orders.GetOrders.Query query, CancellationToken cancellationToken)
    {
        var result = await queryService.Handle(query, cancellationToken);
        return result.ToOkOrProblem();
    }
}
