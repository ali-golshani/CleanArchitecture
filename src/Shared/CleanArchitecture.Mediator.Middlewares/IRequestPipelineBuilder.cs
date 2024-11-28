namespace CleanArchitecture.Mediator.Middlewares;

public interface IRequestPipelineBuilder<TRequest, TResponse>
{
    IRequestProcessor<TRequest, TResponse> EntryProcessor { get; }
}