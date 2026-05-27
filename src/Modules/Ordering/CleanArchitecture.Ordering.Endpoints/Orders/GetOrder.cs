namespace CleanArchitecture.Ordering.Endpoints.Orders;

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

    private static
        async Task<Results<Ok<Order>, ProblemHttpResult>>
        Handle(IQueryService queryService, int orderId, CancellationToken cancellationToken)
    {
        var query = new Queries.Orders.GetOrder.Query
        {
            OrderId = orderId,
        };

        var result = await queryService.Handle(query, cancellationToken);
        return result.ToOkOrNotFoundOrProblem();
    }
}
