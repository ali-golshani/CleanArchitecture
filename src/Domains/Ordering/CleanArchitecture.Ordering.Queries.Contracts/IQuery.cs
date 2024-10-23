namespace CleanArchitecture.Ordering.Queries;

public interface IQuery<TRequest, TResponse> : Mediator.IRequest<TRequest, TResponse>
{ }
