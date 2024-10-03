namespace CleanArchitecture.Authorization;

public interface IPermissionRule<in T>
{
    ValueTask<bool> IsPermit(Actor? actor, T content);
}
