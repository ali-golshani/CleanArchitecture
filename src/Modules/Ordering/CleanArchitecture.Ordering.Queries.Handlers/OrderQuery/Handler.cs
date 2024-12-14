using CleanArchitecture.Ordering.Domain.Repositories;
using Framework.Exceptions;
using Framework.Mediator.Requests;
using Framework.Results;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Ordering.Queries.OrderQuery;

internal sealed class Handler : IRequestHandler<Query, Models.Order?>
{
    private readonly IOrderingQueryDb db;

    public Handler(IOrderingQueryDb db)
    {
        this.db = db;
    }

    public async Task<Result<Models.Order?>> Handle(Query request, CancellationToken cancellationToken)
    {
        var order = await GetOrder(request as FilteredQuery ?? throw new ProgrammerException());
        return order?.Convert();
    }

    private async Task<Domain.Order?> GetOrder(FilteredQuery query)
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

        return await set.FirstOrDefaultAsync(x => x.OrderId == query.OrderId);
    }
}
