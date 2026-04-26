namespace CleanArchitecture.Authorization;

public interface IAccessControl<in T>
{
    ValueTask<AccessLevel> AccessLevel(Actor? actor, T content);
}
