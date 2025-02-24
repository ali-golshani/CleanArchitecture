using Framework.Mediator.Exceptions;
using Framework.Mediator.Extensions;
using Framework.Mediator.Middlewares;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Mediator;

internal sealed class Mediator(IServiceProvider serviceProvider) : IMediator
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public Task<Result<TResponse>> Send<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        where TRequest : IRequest<TRequest, TResponse>
    {
        var pipelines = serviceProvider.GetServices<IPipeline<TRequest, TResponse>>();
        var pipeline = pipelines.FirstOrDefault();

        if (pipeline is null)
        {
            throw new RequestPipelineIsNotRegisteredException<TRequest>();
        }

        return pipeline.Handle(request.AsRequestType(), cancellationToken);
    }
}