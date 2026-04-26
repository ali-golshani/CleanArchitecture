namespace CleanArchitecture.Authorization;

public abstract class AccessControlByPermissionRules<T> : AccessControlBase<T>
{
    protected abstract IPermissionRule<T>[] PermissionRules(T content);

    protected override async ValueTask<bool> IsAuthorized(Actor? actor, T content)
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
}