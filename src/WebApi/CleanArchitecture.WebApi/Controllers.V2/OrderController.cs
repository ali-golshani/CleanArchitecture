using Asp.Versioning;
using CleanArchitecture.Ordering.Queries;
using CleanArchitecture.Ordering.Queries.Models;
using CleanArchitecture.WebApi.Shared.Versioning;
using Framework.WebApi.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers.V2;

[ApiVersion(Versions.V2)]
public class OrderController : BaseController
{
    [HttpGet("{orderId:int}")]
    public async
        Task<Results<Ok<Order>, NotFound, ProblemHttpResult>>
        Get(IQueryService queryService, int orderId, CancellationToken cancellationToken)
    {
        var query = new Ordering.Queries.OrderQuery.Query
        {
            OrderId = orderId,
        };

        var result = await queryService.Handle(query, cancellationToken);

        if (result.IsFailure)
        {
            return result.Errors.ToProblemResult();
        }
        else if (result.Value is null)
        {
            return TypedResults.NotFound();
        }
        else
        {
            return TypedResults.Ok(result.Value);
        }
    }

}
