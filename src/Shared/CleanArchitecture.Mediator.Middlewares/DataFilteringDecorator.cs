using CleanArchitecture.Authorization;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class DataFilteringDecorator<TRequest, TResponse> :
    IRequestProcessor<TRequest, TResponse>
{
    private readonly IRequestProcessor<TRequest, TResponse> next;
    private readonly IDataFilter<TResponse>[] dataFilters;

    public DataFilteringDecorator(
        IRequestProcessor<TRequest, TResponse> next,
        IEnumerable<IDataFilter<TResponse>> dataFilters)
    {
        this.next = next;
        this.dataFilters = dataFilters?.ToArray() ?? [];
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context)
    {
        var response = await next.Handle(context);

        if (response.IsFailure)
        {
            return response;
        }

        var value = response.Value;

        if (value is null)
        {
            return response;
        }

        var actor = context.Actor;

        foreach (var filter in dataFilters)
        {
            value = filter.Filter(actor, value);
        }

        return value;
    }
}