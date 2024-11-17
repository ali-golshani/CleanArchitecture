namespace CleanArchitecture.Authorization;

public abstract class AccessVerifierByPermissionRules<TRequest> : IAccessVerifier<TRequest>
{
    protected abstract IPermissionRule<TRequest>[] PermissionRules(TRequest request);

    public async virtual ValueTask<bool> IsAccessible(Actor? actor, TRequest request)
    {
        foreach (var rule in PermissionRules(request))
        {
            if (await rule.IsPermit(actor, request))
            {
                return true;
            }
        }

        return false;
    }

    public virtual ValueTask<bool> IsMatch(Actor? actor) => ValueTask.FromResult(true);
}