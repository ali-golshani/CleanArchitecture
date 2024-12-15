namespace CleanArchitecture.Mediator.Middlewares;

public interface IPipelineBuilder<TRequest, TResponse>
{
    IRequestProcessor<TRequest, TResponse> EntryProcessor { get; }
}
