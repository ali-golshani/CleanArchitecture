using CleanArchitecture.Actors;
using CleanArchitecture.Mediator.Middlewares;
using Framework.Results;

namespace CleanArchitecture.Ordering.Application.UseCase;

internal sealed class QueryUseCase<TRequest, TResponse> :
    UseCaseBase<TRequest, TResponse>
    where TRequest : QueryBase, IQuery<TRequest, TResponse>
{
    private const string LoggingDomain = nameof(Ordering);

    private readonly IRequestProcessor<TRequest, TResponse> processor;

    public QueryUseCase(
        IActorResolver actorResolver,
        QueryProcessorPipelineBuilder<TRequest, TResponse> pipelineBuilder)
        : base(actorResolver)
    {
        processor = pipelineBuilder.Processor;
    }

    public override async Task<Result<TResponse>> Handle(RequestContext<TRequest> context)
    {
        return await processor.Handle(context);
    }
}
