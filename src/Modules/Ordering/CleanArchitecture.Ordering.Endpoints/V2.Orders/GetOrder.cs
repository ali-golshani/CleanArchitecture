namespace CleanArchitecture.Ordering.Endpoints.V2.Orders;

public sealed class GetOrder : IMinimalEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app
            .MapGet("orders/{orderId:int}", Handle)
            .WithTags("Orders")
            .WithDescription("Get Order by Id")
            .RequireAuthorization()
            ;
    }

    private static async
        Task<Results<Ok<Order>, NotFound, ProblemHttpResult>>
        Handle(IQueryService queryService, int orderId, CancellationToken cancellationToken)
    {
        var query = new Queries.Orders.GetOrder.Query
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
