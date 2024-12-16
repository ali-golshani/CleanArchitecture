using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Requests;

namespace Infrastructure.CommoditySystem.Pipeline;

internal sealed class RequestPipelineBuilder<TRequest, TResponse>
    : IPipelineBuilder<TRequest, TResponse>
    where TRequest : RequestBase, IRequest<TRequest, TResponse>
{
    public RequestPipelineBuilder(
        RequestHandlingProcessor<TRequest, TResponse> processor,
        ExceptionTranslationMiddleware<TRequest, TResponse> exceptionTranslation,
        RequestAuditMiddleware<TRequest, TResponse> audit)
    {
        EntryProcessor = PipelineBuilder.EntryProcessor
        (
            processor, 
            exceptionTranslation, 
            audit
        );
    }

    public IRequestProcessor<TRequest, TResponse> EntryProcessor { get; }
}
