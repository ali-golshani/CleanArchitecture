using Framework.Mediator;
using Framework.Mediator.Extensions;
using Framework.Results;
using Infrastructure.CommoditySystem.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CommoditySystem;

internal class MockCommoditySystem(IServiceProvider serviceProvider) : ICommoditySystem
{
    public async Task<Result<TResponse>> Handle<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        where TRequest : RequestBase, IRequest<TRequest, TResponse>
    {
        var handler = serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        return await handler.Handle(request.AsRequestType(), cancellationToken);
    }
}