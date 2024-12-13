namespace CleanArchitecture.Authorization;

public static class Extensions
{
    public static async ValueTask<bool> HasPermission<TRequest>(
        this IEnumerable<IAccessControl<TRequest>> controls,
        Actor? actor,
        TRequest request)
    {
        foreach (var control in controls)
        {
            if (await control.Clause(actor) && !await control.IsAuthorized(actor, request))
            {
                return false;
            }
        }

        return true;
    }
}
