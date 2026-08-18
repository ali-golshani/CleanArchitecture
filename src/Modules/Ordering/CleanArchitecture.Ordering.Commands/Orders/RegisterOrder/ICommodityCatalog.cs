using CleanArchitecture.Ordering.Domain.Orders;
using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.Orders.RegisterOrder;

public interface ICommodityCatalog
{
    Task<Result<Commodity?>> Find(int commodityId, CancellationToken cancellationToken);
}
