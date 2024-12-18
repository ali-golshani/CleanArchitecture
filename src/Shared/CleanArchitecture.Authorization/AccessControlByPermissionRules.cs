namespace CleanArchitecture.Authorization;

public abstract class AccessControlByPermissionRules<T> : IAccessControl<T>
{
    protected abstract IPermissionRule<T>[] PermissionRules(T content);

    public virtual async ValueTask<bool> IsAuthorized(Actor? actor, T content)
    {
        foreach (var rule in PermissionRules(content))
        {
            if (await rule.HasPermission(actor, content))
            {
                return true;
            }
        }

        return false;
    }

    public virtual ValueTask<bool> Clause(Actor? actor) => ValueTask.FromResult(true);
}