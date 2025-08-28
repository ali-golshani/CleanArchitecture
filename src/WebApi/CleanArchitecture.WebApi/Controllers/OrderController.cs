using CleanArchitecture.Ordering.Commands;
using CleanArchitecture.Ordering.Commands.Orders.RegisterOrderCommand;
using CleanArchitecture.Ordering.Queries;
using CleanArchitecture.Ordering.Queries.Models;
using CleanArchitecture.Ordering.Queries.Orders.OrdersQuery;
using Framework.Queries;
using Framework.Results.Extensions;
using Framework.WebApi.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers;

public class OrderController : BaseController
{
    /// <summary>
    /// Search Orders
    /// </summary>
    [HttpGet]
    public
        Task<Results<Ok<PaginatedItems<Order>>, ProblemHttpResult>>
        Get(
        IQueryService queryService,
        [FromQuery] Query query,
        CancellationToken cancellationToken)
    {
        return
            queryService
            .Handle(query, cancellationToken)
            .AsTypedResults();
    }

    /// <summary>
    /// Get Order by Id
    /// </summary>
    [HttpGet("{orderId:int}")]
    public
        Task<Results<Ok<Order>, ProblemHttpResult>>
        Get(IQueryService queryService, int orderId, CancellationToken cancellationToken)
    {
        var query = new Ordering.Queries.Orders.OrderQuery.Query
        {
            OrderId = orderId,
        };

        return
            queryService
            .Handle(query, cancellationToken)
            .NotFoundIfNull(new Ordering.Commands.Errors.OrderNotFoundError(query.OrderId))
            .AsTypedResults();
    }

    /// <summary>
    /// Register Order
    /// </summary>
    [HttpPost]
    public
        Task<Results<NoContent, ProblemHttpResult>>
        Add(
        ICommandService commandService,
        Command command,
        CancellationToken cancellationToken)
    {
        return
            commandService
            .Handle(command, cancellationToken)
            .AsTypedResults();
    }
}
