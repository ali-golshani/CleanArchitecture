using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Requests;
using Infrastructure.RequestAudit;
using Microsoft.Extensions.Logging;

namespace Infrastructure.CommoditySystem.Pipeline;

internal sealed class RequestPipelineBuilder<TRequest, TResponse>
    : IPipelineBuilder<TRequest, TResponse>
    where TRequest : RequestBase, IRequest<TRequest, TResponse>
{
    private const string LoggingDomain = nameof(CommoditySystem);

    public RequestPipelineBuilder(
        IRequestHandler<TRequest, TResponse> handler,
        RequestAuditAgent requestAudit,
        ILogger<RequestPipelineBuilder<TRequest, TResponse>> logger)
    {
        var requestHandling = new RequestHandlingProcessor<TRequest, TResponse>(handler);
        var audit = new RequestAuditFilter<TRequest, TResponse>(requestAudit, LoggingDomain, logger);
        var exceptionTranslation = new ExceptionTranslationFilter<TRequest, TResponse>();

        var filters = new IFilter<TRequest, TResponse>[]
        {
            exceptionTranslation,
            audit,
        };

        EntryProcessor = new Pipeline<TRequest, TResponse>(filters, requestHandling);
    }

    public IRequestProcessor<TRequest, TResponse> EntryProcessor { get; }
}
