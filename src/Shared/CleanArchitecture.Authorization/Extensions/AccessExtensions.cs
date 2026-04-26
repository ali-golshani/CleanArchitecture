namespace CleanArchitecture.Authorization.Extensions;

public static class AccessExtensions
{
    public static async ValueTask<bool> IsAccessDenied<T>(
        this IAccessControl<T> control,
        Actor? actor,
        T content)
    {
        return await control.AccessLevel(actor, content) == AccessLevel.Denied;
    }
}
