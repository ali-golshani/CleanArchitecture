using CleanArchitecture.Ordering.Domain.Repositories;
using Framework.Mediator;
using Framework.Persistence.Extensions;
using Framework.Queries;
using Framework.Results;

namespace CleanArchitecture.Ordering.Queries.Orders.OrdersQuery;

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

    private IQueryable<Domain.Orders.Order> SelectOrders(Query query)
    {
        IQueryable<Domain.Orders.Order> set = db.QuerySet<Domain.Orders.Order>();

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

        switch (query.OrderBy)
        {
            case Models.OrderOrderBy.BrokerId:
                return set.OrderByDescending(x => x.BrokerId);

            case Models.OrderOrderBy.OrderId:
            default:
                return set.OrderByDescending(x => x.OrderId);
        }
    }
}
