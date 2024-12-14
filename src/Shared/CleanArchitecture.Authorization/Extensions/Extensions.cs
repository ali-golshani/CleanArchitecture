namespace CleanArchitecture.Authorization;

public static class Extensions
{
    public static async ValueTask<bool> IsAccessDenied<T>(
        this IAccessControl<T> control,
        Actor? actor,
        T content)
    {
        return await control.Clause(actor) && !await control.IsAuthorized(actor, content);
    }

    public static async ValueTask<bool> IsAccessDenied<T>(
        this IEnumerable<IAccessControl<T>> controls,
        Actor? actor,
        T content)
    {
        foreach (var control in controls)
        {
            if (await control.IsAccessDenied(actor, content))
            {
                return true;
            }
        }

        return false;
    }
}
