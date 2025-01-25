using Framework.Mediator;

namespace CleanArchitecture.Ordering.Queries;

public interface IQuery<TRequest, TResponse> : IRequest<TRequest, TResponse>
{ }
