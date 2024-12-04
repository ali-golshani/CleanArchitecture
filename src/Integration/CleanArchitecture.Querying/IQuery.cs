using Framework.Mediator.Requests;

namespace CleanArchitecture.Querying;

public interface IQuery<TRequest, TResponse> : IRequest<TRequest, TResponse>
{ }
