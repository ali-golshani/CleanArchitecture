using CleanArchitecture.Actors;
using Framework.Mediator.Requests;

namespace CleanArchitecture.Mediator.Middlewares;

public abstract class RequestPipeline<TRequest, TResponse>
    where TRequest : IRequest<TRequest, TResponse>
{
    protected readonly IActorResolver actorResolver;
    protected readonly IRequestProcessor<TRequest, TResponse> processor;

    protected RequestPipeline(IActorResolver actorResolver, IRequestPipelineBuilder<TRequest, TResponse> pipelineBuilder)
    {
        this.actorResolver = actorResolver;
        processor = pipelineBuilder.EntryProcessor;
    }

    public virtual async Task<Result<TResponse>> Handle(RequestContext<TRequest> context)
    {
        return await processor.Handle(context);
    }

    public async Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var actor = actorResolver.Actor;

        if (actor is null)
        {
            return ActorNotSpecifiedError.Default;
        }

        return await Handle(actor, request, cancellationToken);
    }

    public async Task<Result<TResponse>> Handle(Actor actor, TRequest request, CancellationToken cancellationToken)
    {
        var context = new RequestContext<TRequest>
        {
            Actor = actor,
            Request = request,
            CancellationToken = cancellationToken,
        };

        return await Handle(context);
    }
}
