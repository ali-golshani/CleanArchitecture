namespace CleanArchitecture.Authorization;

public interface IPermissionRule<in T>
{
    ValueTask<bool> HasPermission(Actor? actor, T content);
}
