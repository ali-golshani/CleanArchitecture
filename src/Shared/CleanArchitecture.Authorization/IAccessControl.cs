namespace CleanArchitecture.Authorization;

public interface IAccessControl<in T>
{
    ValueTask<bool> Clause(Actor? actor);
    ValueTask<bool> IsAuthorized(Actor? actor, T content);
}