using Framework.Mediator;
using Framework.Mediator.Middlewares;
using Infrastructure.CommoditySystem.Requests;
using Infrastructure.RequestAudit;

namespace Infrastructure.CommoditySystem.Pipelines;

internal sealed class RequestPipeline<TRequest, TResponse> : Pipeline<TRequest, TResponse>
    where TRequest : RequestBase, IRequest<TRequest, TResponse>
{
    public RequestPipeline(
        IRequestHandler<TRequest, TResponse> handler,
        ExceptionTranslationMiddleware<TRequest, TResponse> exceptionTranslation,
        RequestAuditMiddleware<TRequest, TResponse> audit)
        : base(handler, exceptionTranslation, audit)
    { }
}