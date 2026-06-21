using CleanArchitecture.Authorization.Claims;
using CleanArchitecture.Authorization.WebApi.Policies.Permissions;
using CleanArchitecture.Querying.Services;
using Framework.Results.Extensions;
using Framework.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Routing;

namespace CleanArchitecture.Querying.GetOrders;

public sealed class Endpoint : IMinimalEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app
            .MapGet("orders", Handle)
            .WithTags("Orders")
            .WithDescription("Query Orders")
            .WithODataResult()
            .RequireAuthorization(new PermissionAuthorizeAttribute(Permission.ReadOrders))
            ;
    }

    private static async Task<IQueryable<Order>> Handle(
        IQueryService queryService,
        [AsParameters] Query query,
        CancellationToken cancellationToken)
    {
        return await
            queryService
            .Handle(query, cancellationToken)
            .ThrowIfFailure();
    }
}
