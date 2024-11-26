namespace CleanArchitecture.ProcessManager;

public interface IRequest<TRequest, TResponse> : Framework.Mediator.Requests.IRequest<TRequest, TResponse>
{ }
