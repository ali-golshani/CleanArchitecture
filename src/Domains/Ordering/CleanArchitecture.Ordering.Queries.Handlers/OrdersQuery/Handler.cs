using CleanArchitecture.Ordering.Domain.Repositories;
using Framework.Mediator.Requests;
using Framework.Persistence.Extensions;
using Framework.Queries;
using Framework.Results;

namespace CleanArchitecture.Ordering.Queries.OrdersQuery;

internal sealed class Handler : IRequestHandler<Query, PaginatedItems<Models.Order>>
{
    private readonly IOrderingQueryDb db;

    public Handler(IOrderingQueryDb db)
    {
        this.db = db;
    }

    public async Task<Result<PaginatedItems<Models.Order>>> Handle(Query request, CancellationToken cancellationToken)
    {
        var orders = SelectOrders(request);
        return await orders.Materialize(x => x.Convert(), request.PageIndex, request.PageSize);
    }

    private IQueryable<Domain.Order> SelectOrders(Query query)
    {
        IQueryable<Domain.Order> set = db.QuerySet<Domain.Order>();

        if (query.CustomerId is not null)
        {
            set = set.Where(x => x.CustomerId == query.CustomerId.Value);
        }

        if (query.BrokerId is not null)
        {
            set = set.Where(x => x.BrokerId == query.BrokerId.Value);
        }

        if (query.CommodityId is not null)
        {
            set = set.Where(x => x.Commodity.CommodityId == query.CommodityId.Value);
        }

        if (query.OrderStatus is not null)
        {
            set = set.Where(x => x.Status == query.OrderStatus.Value);
        }

        return set.OrderByDescending(x => x.OrderId);
    }
}
