using CleanArchitecture.Querying.Persistence;
using Framework.Mediator;
using Framework.Results;
using Framework.Results.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Querying.OrdersQuery;

internal sealed class Handler(EmptyDbContext db) : IRequestHandler<Query, IQueryable<Order>>
{
    private static readonly string SqlQuery = Properties.Resources.OrdersSqlView;
    private readonly EmptyDbContext db = db;

    public async Task<Result<IQueryable<Order>>> Handle(Query request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        IQueryable<Order> orders = db.Database.SqlQueryRaw<Order>(SqlQuery);

        if (request.CustomerId != null)
        {
            orders = orders.Where(x => x.CustomerId == request.CustomerId.Value);
        }

        if (request.BrokerId != null)
        {
            orders = orders.Where(x => x.BrokerId == request.BrokerId.Value);
        }

        return orders.AsResult();
    }
}