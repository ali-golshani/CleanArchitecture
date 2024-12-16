using CleanArchitecture.Mediator.Middlewares.Transformers;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class TransformingMiddleware<TRequest, TResponse> :
    IMiddleware<TRequest, TResponse>
{
    private readonly ITransformer<TRequest>[] requestTransformers;
    private readonly ITransformer<TResponse>[] responseTransformers;

    public TransformingMiddleware(
        IEnumerable<ITransformer<TRequest>> requestTransformers,
        IEnumerable<ITransformer<TResponse>> responseTransformers)
    {
        this.requestTransformers = requestTransformers?.ToArray() ?? [];
        this.responseTransformers = responseTransformers?.ToArray() ?? [];
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IRequestProcessor<TRequest, TResponse> next)
    {
        var actor = context.Actor;
        var request = context.Request;
        var cancellationToken = context.CancellationToken;

        foreach (var filter in requestTransformers.OrderBy(x => x.Order))
        {
            var result = await filter.Transform(request, actor, cancellationToken);

            if (result.IsFailure)
            {
                return result.Errors;
            }

            request = result.Value!;
        }

        context = new RequestContext<TRequest>
        {
            Actor = actor,
            Request = request,
            CancellationToken = cancellationToken
        };

        var responseResult = await next.Handle(context);

        if (responseResult.IsFailure || responseResult.Value is null)
        {
            return responseResult;
        }

        var response = responseResult.Value;

        foreach (var filter in responseTransformers.OrderBy(x => x.Order))
        {
            var result = await filter.Transform(response, actor, cancellationToken);

            if (result.IsFailure)
            {
                return result;
            }

            response = result.Value!;
        }

        return response;
    }
}