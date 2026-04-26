namespace CleanArchitecture.Authorization;

public sealed class AccessResolver<T>(IEnumerable<IAccessControl<T>>? accessControls)
{
    private readonly IAccessControl<T>[] accessControls = accessControls?.ToArray() ?? [];

    public async ValueTask<AccessLevel> AccessLevel(Actor? actor, T content)
    {
        var result = Authorization.AccessLevel.Undetermined;

        foreach (var accessControl in accessControls)
        {
            var accessLevel = await accessControl.AccessLevel(actor, content);

            switch (accessLevel)
            {
                case Authorization.AccessLevel.Denied:
                    return Authorization.AccessLevel.Denied;

                case Authorization.AccessLevel.Granted:
                    result = Authorization.AccessLevel.Granted;
                    break;
            }
        }

        return result;
    }
}
