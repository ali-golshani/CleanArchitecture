using Framework.Mediator.Requests;

namespace CleanArchitecture.Ordering.Commands;

public interface ICommand<TRequest, TResponse> : IRequest<TRequest, TResponse>
{ }
