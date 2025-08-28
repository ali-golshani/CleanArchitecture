namespace Framework.Mediator;

public interface IRequest<in TRequest, TResponse> : Minimal.Mediator.IRequest<TRequest, Result<TResponse>> { }