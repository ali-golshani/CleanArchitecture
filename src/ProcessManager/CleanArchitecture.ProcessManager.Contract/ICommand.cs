using Framework.Mediator.Requests;

namespace CleanArchitecture.ProcessManager;

public interface ICommand<TRequest, TResponse> : IRequest<TRequest, TResponse>
{ }
