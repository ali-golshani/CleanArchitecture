namespace CleanArchitecture.Authorization;

public interface IFilter<TRequest, TResponse>
{
    int Order => 1;
    void Filter(TRequest request, Actor actor) { }
    TResponse Filter(TResponse response, Actor actor) => response;
}
