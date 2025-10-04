using CleanArchitecture.Ordering.Queries;
using CleanArchitecture.Ordering.Commands;
using CleanArchitecture.Ordering.Queries.Models;
using Framework.Queries;
using Framework.WebApi.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using GetOrder = CleanArchitecture.Ordering.Queries.Orders.GetOrder;
using GetOrders = CleanArchitecture.Ordering.Queries.Orders.GetOrders;
using RegisterOrder = CleanArchitecture.Ordering.Commands.Orders.RegisterOrder;
using CleanArchitecture.Authorization.WebApi.Policies.Scopes;
using CleanArchitecture.Authorization.WebApi.Policies.Permissions;

namespace CleanArchitecture.WebApi.Controllers;

[ScopeAuthorize(Scopes.Orders)]
public class OrderController : BaseController
{
    /// <summary>
    /// Search Orders
    /// </summary>
    [PermissionAuthorize(Permission.ReadOrders)]
    [HttpGet]
    public
        Task<Results<Ok<PaginatedItems<Order>>, ProblemHttpResult>>
        Get(IQueryService queryService, [FromQuery] GetOrders.Query query, CancellationToken cancellationToken)
    {
        return
            queryService
            .Handle(query, cancellationToken)
            .ToOkOrProblem();
    }

    /// <summary>
    /// Get Order by Id
    /// </summary>
    [PermissionAuthorize(Permission.ReadOrders)]
    [HttpGet("{orderId:int}")]
    public
        Task<Results<Ok<Order>, NotFound, ProblemHttpResult>>
        Get(IQueryService queryService, int orderId, CancellationToken cancellationToken)
    {
        var query = new GetOrder.Query
        {
            OrderId = orderId,
        };

        return
            queryService
            .Handle(query, cancellationToken)
            .ToOkOrNotFoundOrProblem();
    }

    /// <summary>
    /// Register Order
    /// </summary>
    [PermissionAuthorize(Permission.RegisterOrder)]
    [HttpPost]
    public
        Task<Results<NoContent, ProblemHttpResult>>
        Add(ICommandService commandService, RegisterOrder.Command command, CancellationToken cancellationToken)
    {
        return
            commandService
            .Handle(command, cancellationToken)
            .ToNoContentOrProblem();
    }
}
