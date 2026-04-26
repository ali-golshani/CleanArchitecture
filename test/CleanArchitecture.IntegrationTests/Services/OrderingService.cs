using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Commands;
using CleanArchitecture.Ordering.Queries;
using Framework.DependencyInjection;
using Framework.Results;
using Framework.Results.Extensions;

namespace CleanArchitecture.IntegrationTests.Services;

internal class OrderingService(IQueryService queryService, ICommandService commandService) : ITransientService
{
    protected static readonly Actor Tester = new Programmer("tester", "Test App");

    private readonly IQueryService queryService = queryService;
    private readonly ICommandService commandService = commandService;

    protected Task<Result<TResponse>> Handle<TRequest, TResponse>(IQuery<TRequest, TResponse> query, CancellationToken cancellationToken)
        where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        return queryService.Handle(Tester, query, cancellationToken);
    }

    protected Task<Result<TResponse>> Handle<TRequest, TResponse>(ICommand<TRequest, TResponse> command, CancellationToken cancellationToken)
        where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        return commandService.Handle(Tester, command, cancellationToken);
    }

    public async Task<Result<int>> RegisterNextOrder(int customerId, int commodityId, CancellationToken cancellationToken)
    {
        var ordersResult = await Handle(new Ordering.Queries.Orders.GetOrders.Query
        {
            OrderBy = Ordering.Queries.Models.OrderOrderBy.OrderId,
            PageSize = 1
        }, cancellationToken);

        var orders = ordersResult.ThrowIsFailure();

        var orderId = orders.Items.Count > 0 ? orders.Items.Max(x => x.OrderId) : 1;
        orderId += 1;

        var command = new Fakers.Commands.RegisterOrder
        (
            orderId: orderId,
            customerId: customerId,
            commodityId: commodityId
        ).Generate();

        var result = await Handle(command, cancellationToken);

        if (result.IsFailure)
        {
            return result.Errors;
        }

        return orderId;
    }

    public async Task<Result<Empty>> SubmitOrder(int orderId, CancellationToken cancellationToken)
    {
        var command = new Fakers.Commands.SubmitOrder(orderId).Generate();

        return await Handle(command, cancellationToken);
    }
}
