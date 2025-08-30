using Framework.Mediator;
using Framework.Results;

namespace Infrastructure.CommoditySystem.Requests;

internal abstract class UseCase<TRequest, TResponse>(IRequestHandler<TRequest, TResponse> handler) : IUseCase<TRequest, TResponse>
    where TRequest : IRequest<TRequest, TResponse>
{
    private readonly IRequestHandler<TRequest, TResponse> handler = handler;

    public async Task<Result<TResponse>> Execute(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return await handler.Handle(request, cancellationToken);
        }
        catch (CommoditySystemException)
        {
            throw;
        }
        catch (Exception exp)
        {
            throw new CommoditySystemException(exp);
        }
    }
}