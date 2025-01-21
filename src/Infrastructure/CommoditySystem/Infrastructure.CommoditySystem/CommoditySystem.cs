using Framework.Mediator;
using Framework.Mediator.Extensions;
using Framework.Results;
using Infrastructure.CommoditySystem.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CommoditySystem;

internal class CommoditySystem(IServiceProvider serviceProvider) : ICommoditySystem
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public Task<Result<TResponse>> Handle<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        where TRequest : RequestBase, IRequest<TRequest, TResponse>
    {
        var pipeline = serviceProvider.GetRequiredService<RequestPipeline<TRequest, TResponse>>();
        return pipeline.Handle(request.AsRequestType(), cancellationToken);
    }
}