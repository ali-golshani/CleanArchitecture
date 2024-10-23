namespace CleanArchitecture.Ordering.Commands;

public interface ICommand<TRequest, TResponse> : Mediator.IRequest<TRequest, TResponse>
{ }
