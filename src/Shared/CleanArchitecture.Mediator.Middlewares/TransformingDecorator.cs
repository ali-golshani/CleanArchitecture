using CleanArchitecture.Mediator.Middlewares.Transformers;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class TransformingDecorator<TRequest, TResponse> :
    IRequestProcessor<TRequest, TResponse>
{
    private readonly IRequestProcessor<TRequest, TResponse> next;
    private readonly ITransformer<TRequest>[] requestTransformers;
    private readonly ITransformer<TResponse>[] responseTransformers;

    public TransformingDecorator(
        IRequestProcessor<TRequest, TResponse> next,
        IEnumerable<ITransformer<TRequest>> requestTransformers,
        IEnumerable<ITransformer<TResponse>> responseTransformers)
    {
        this.next = next;
        this.requestTransformers = requestTransformers?.ToArray() ?? [];
        this.responseTransformers = responseTransformers?.ToArray() ?? [];
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context)
    {
        var request = context.Request;

        foreach (var transformer in requestTransformers.OrderBy(x => x.Order))
        {
            var result = await transformer.Transform(request, context.Actor, context.CancellationToken);

            if (result.IsFailure)
            {
                return result.Errors;
            }

            request = result.Value!;
        }

        context = new RequestContext<TRequest>
        {
            Actor = context.Actor,
            Request = request,
            CancellationToken = context.CancellationToken
        };

        var responseResult = await next.Handle(context);

        if (responseResult.IsFailure || responseResult.Value is null)
        {
            return responseResult;
        }

        var response = responseResult.Value;

        foreach (var transformer in responseTransformers.OrderBy(x => x.Order))
        {
            var result = await transformer.Transform(response, context.Actor, context.CancellationToken);

            if (result.IsFailure)
            {
                return result;
            }

            response = result.Value!;
        }

        return response;
    }
}