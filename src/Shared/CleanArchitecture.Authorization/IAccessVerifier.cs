namespace CleanArchitecture.Authorization;

public interface IAccessVerifier<in TRequest>
{
    ValueTask<bool> IsMatch(Actor? actor);
    ValueTask<bool> IsAccessible(Actor? actor, TRequest request);
}
