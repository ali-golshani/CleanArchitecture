using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Requests;
using Microsoft.Extensions.Logging;
using Infrastructure.RequestAudit;

namespace Infrastructure.CommoditySystem.Pipeline;

internal sealed class RequestPipelineBuilder<TRequest, TResponse>
    : IRequestPipelineBuilder<TRequest, TResponse>
    where TRequest : RequestBase, IRequest<TRequest, TResponse>
{
    private const string LoggingDomain = nameof(CommoditySystem);

    public RequestPipelineBuilder(
        IRequestHandler<TRequest, TResponse> handler,
        RequestAuditAgent requestAudit,
        ILogger<RequestPipeline<TRequest, TResponse>> logger)
    {
        var requestHandling = new RequestHandlingProcessor<TRequest, TResponse>(handler);
        var audit = new RequestAuditDecorator<TRequest, TResponse>(requestHandling, requestAudit, LoggingDomain, logger);
        var exceptionTranslation = new ExceptionTranslationDecorator<TRequest, TResponse>(audit);

        EntryProcessor = exceptionTranslation;
    }

    public IRequestProcessor<TRequest, TResponse> EntryProcessor { get; }
}
