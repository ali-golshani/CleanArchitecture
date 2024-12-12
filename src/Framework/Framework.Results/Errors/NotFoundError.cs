namespace Framework.Results.Errors;

public class NotFoundError : Error
{
    public NotFoundError(string resourceName, object? resourceKey = null)
        : base(ErrorType.NotFound, ErrorMessage(resourceName, resourceKey))
    {
        ResourceName = resourceName;
        ResourceKey = resourceKey;
    }

    public string ResourceName { get; }
    public object? ResourceKey { get; }

    private static string ErrorMessage(string resourceName, object? resourceKey)
    {
        if (resourceKey is null)
        {
            return $"{resourceName} مورد نظر یافت نشد";
        }
        else
        {
            return $"{resourceName} با شناسه {resourceKey} یافت نشد";
        }
    }
}
