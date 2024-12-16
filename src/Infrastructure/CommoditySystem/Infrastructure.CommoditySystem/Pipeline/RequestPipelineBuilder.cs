using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Requests;
using Infrastructure.RequestAudit;

namespace Infrastructure.CommoditySystem.Pipeline;

internal sealed class RequestPipelineBuilder<TRequest, TResponse>
    : IPipelineBuilder<TRequest, TResponse>
    where TRequest : RequestBase, IRequest<TRequest, TResponse>
{
    public RequestPipelineBuilder(
        IRequestHandler<TRequest, TResponse> handler,
        RequestAuditMiddlewareBuilder auditMiddlewareBuilder)
    {
        var processor = new RequestHandlingProcessor<TRequest, TResponse>(handler);

        var middlewares = new IMiddleware<TRequest, TResponse>[]
        {
            new ExceptionTranslationMiddleware<TRequest, TResponse>(),
            auditMiddlewareBuilder.Build<TRequest, TResponse>(nameof(CommoditySystem)),
        };

        EntryProcessor = PipelineBuilder.EntryProcessor(middlewares, processor);
    }

    public IRequestProcessor<TRequest, TResponse> EntryProcessor { get; }
}
