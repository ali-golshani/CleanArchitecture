using CleanArchitecture.Ordering.Domain.Repositories;
using Framework.Mediator;
using Framework.Mediator.Extensions;
using Framework.Results;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Ordering.Queries.Orders.GetOrder;

internal sealed class Handler(IOrderingQueryDb db) : IRequestHandler<Query, Models.Order?>
{
    private readonly IOrderingQueryDb db = db;

    public async Task<Result<Models.Order?>> Handle(Query request, CancellationToken cancellationToken)
    {
        var order = await GetOrder(request.AsRequestType<FilteredQuery>());
        return order?.Convert();
    }

    private async Task<Domain.Orders.Order?> GetOrder(FilteredQuery query)
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

        return await set.FirstOrDefaultAsync(x => x.OrderId == query.OrderId);
    }
}
