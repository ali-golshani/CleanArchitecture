namespace CleanArchitecture.UserManagement.Application.Requests;

public interface IRequest<TRequest, TResponse> : Framework.Mediator.IRequest<TRequest, TResponse>;
