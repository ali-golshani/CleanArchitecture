using Framework.Mediator;
using Framework.Results;

namespace Infrastructure.CommoditySystem.Requests;

public interface IUseCase<in TRequest, TResponse>
    where TRequest : IRequest<TRequest, TResponse>
{
    Task<Result<TResponse>> Execute(TRequest request, CancellationToken cancellationToken);
}