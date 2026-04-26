using Framework.Mediator;
using Framework.Mediator.Extensions;
using Framework.Results;
using Infrastructure.CommoditySystem.Pipelines;
using Infrastructure.CommoditySystem.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CommoditySystem;

internal sealed class CommoditySystem(IServiceProvider serviceProvider) : ICommoditySystem
{
    public async Task<Result<TResponse>> Handle<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        where TRequest : RequestBase, IRequest<TRequest, TResponse>
    {
        var pipeline = serviceProvider.GetRequiredService<RequestPipeline<TRequest, TResponse>>();
        return await pipeline.Handle(request.AsRequestType(), cancellationToken);
    }
}