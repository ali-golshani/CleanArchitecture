namespace CleanArchitecture.Authorization;

public static class Extensions
{
    public static async ValueTask<bool> IsAccessible<TRequest>(
        this IEnumerable<IAccessVerifier<TRequest>> verifiers,
        Actor? actor,
        TRequest request)
    {
        foreach (var verifier in verifiers)
        {
            if (await verifier.IsMatch(actor) && !await verifier.IsAccessible(actor, request))
            {
                return false;
            }
        }

        return true;
    }
}
