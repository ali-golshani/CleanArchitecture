namespace Framework.Results.Resources;

internal static class ErrorMessageBuilder
{
    public static string NotFound(string resourceName, object? resourceKey)
    {
        if (resourceKey is null)
        {
            return string.Format(ErrorMessages.NotFound_1, resourceName);
        }
        else
        {
            return string.Format(ErrorMessages.NotFound_2, resourceName, resourceKey);
        }
    }

    public static string Duplicate(string resourceName, object? resourceKey)
    {
        if (resourceKey is null)
        {
            return string.Format(ErrorMessages.Duplicate_1, resourceName);
        }
        else
        {
            return string.Format(ErrorMessages.Duplicate_2, resourceName, resourceKey);
        }
    }
}
