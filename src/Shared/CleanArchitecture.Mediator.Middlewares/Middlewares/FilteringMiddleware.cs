using CleanArchitecture.Authorization;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class FilteringMiddleware<TRequest, TResponse> :
    IMiddleware<TRequest, TResponse>
{
    private readonly IFilter<TRequest, TResponse>[] filters;

    public FilteringMiddleware(
        IEnumerable<IFilter<TRequest, TResponse>> filters)
    {
        this.filters = filters?.OrderBy(x => x.Order).ToArray() ?? [];
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IRequestProcessor<TRequest, TResponse> next)
    {
        var actor = context.Actor;
        var request = context.Request;
        var cancellationToken = context.CancellationToken;

        foreach (var filter in filters)
        {
            request = filter.Filter(request, actor);
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

        foreach (var filter in filters)
        {
            response = filter.Filter(response, actor);
        }

        return response;
    }
}