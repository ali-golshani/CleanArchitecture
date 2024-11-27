using CleanArchitecture.Actors;
using Framework.Mediator.Requests;

namespace CleanArchitecture.Mediator.Middlewares;

public abstract class UseCaseBase<TRequest, TResponse>
    where TRequest : IRequest<TRequest, TResponse>
{
    public abstract Task<Result<TResponse>> Handle(UseCaseContext<TRequest> context);

    protected readonly IActorResolver actorResolver;

    protected UseCaseBase(IActorResolver actorResolver)
    {
        this.actorResolver = actorResolver;
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
        var context = new UseCaseContext<TRequest>
        {
            Actor = actor,
            Request = request,
            CancellationToken = cancellationToken,
        };

        return await Handle(context);
    }
}
