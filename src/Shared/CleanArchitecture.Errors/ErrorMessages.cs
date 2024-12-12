namespace CleanArchitecture.Errors;

internal static class ErrorMessages
{
    public static string Duplicate(string resourceName, object? resourceKey)
    {
        if (resourceKey is null)
        {
            return $"{resourceName} با شناسه درخواستی وجود دارد";
        }
        else
        {
            return $"{resourceName} با شناسه {resourceKey} وجود دارد";
        }
    }
}
