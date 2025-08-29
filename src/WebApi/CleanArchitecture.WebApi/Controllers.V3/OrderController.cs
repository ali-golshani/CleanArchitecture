using Asp.Versioning;
using CleanArchitecture.Ordering.Queries.Models;
using CleanArchitecture.WebApi.Shared.Versioning;
using Framework.WebApi.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using GetOrder = CleanArchitecture.Ordering.Queries.Orders.GetOrder;
using RegisterOrder = CleanArchitecture.Ordering.Commands.Orders.RegisterOrder;

namespace CleanArchitecture.WebApi.Controllers.V3;

[ApiVersion(Versions.V3)]
public class OrderController : BaseController
{
    [HttpGet("{orderId:int}")]
    public
        Task<Results<Ok<Order>, NotFound, ProblemHttpResult>>
        Get(GetOrder.UseCase useCase, int orderId, CancellationToken cancellationToken)
    {
        var query = new GetOrder.Query
        {
            OrderId = orderId,
        };

        return
            useCase
            .Execute(query, cancellationToken)
            .ToOkOrNotFoundOrProblem();
    }

    /// <summary>
    /// Register Order
    /// </summary>
    [HttpPost]
    public
        Task<Results<NoContent, ProblemHttpResult>>
        Add(RegisterOrder.UseCase useCase, RegisterOrder.Command command, CancellationToken cancellationToken)
    {
        return
            useCase
            .Execute(command, cancellationToken)
            .ToNoContentOrProblem();
    }
}
