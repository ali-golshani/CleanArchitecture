using CleanArchitecture.Querying.Persistence;
using Framework.Mediator.Requests;
using Framework.Results;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Querying.OrdersQuery;

internal sealed class Handler(EmptyDbContext db) : IRequestHandler<Query, IQueryable<Order>>
{
    private readonly EmptyDbContext db = db;

    public async Task<Result<IQueryable<Order>>> Handle(Query request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        IQueryable<Order> result = db.Database.SqlQueryRaw<Order>(SqlQuery);

        if (request.CustomerId != null)
        {
            result = result.Where(x => x.CustomerId == request.CustomerId.Value);
        }

        if (request.BrokerId != null)
        {
            result = result.Where(x => x.BrokerId == request.BrokerId.Value);
        }

        return Result<IQueryable<Order>>.Success(result);
    }

    private static string SqlQuery => Properties.Resources.OrdersSqlView;
}