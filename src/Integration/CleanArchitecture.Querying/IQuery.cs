using Framework.Mediator;

namespace CleanArchitecture.Querying;

public interface IQuery<TRequest, TResponse> : IRequest<TRequest, TResponse>
{ }
