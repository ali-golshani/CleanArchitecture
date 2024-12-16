using CleanArchitecture.Actors;

namespace CleanArchitecture.Mediator.Middlewares;

public interface IFilter<TRequest, TResponse>
{
    int Order => 1;
    TRequest Filter(TRequest request, Actor actor) => request;
    TResponse Filter(TResponse response, Actor actor) => response;
}
