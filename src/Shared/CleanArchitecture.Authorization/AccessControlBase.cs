namespace CleanArchitecture.Authorization;

public abstract class AccessControlBase<T> : IAccessControl<T>
{
    protected abstract ValueTask<bool> IsAuthorized(Actor? actor, T content);

    protected virtual ValueTask<bool> Clause(Actor? actor) => ValueTask.FromResult(true);

    public async ValueTask<AccessLevel> AccessLevel(Actor? actor, T content)
    {
        if (await Clause(actor))
        {
            if (await IsAuthorized(actor, content))
            {
                return Authorization.AccessLevel.Granted;
            }
            else
            {
                return Authorization.AccessLevel.Denied;
            }
        }
        else
        {
            return Authorization.AccessLevel.Undetermined;
        }
    }
}
