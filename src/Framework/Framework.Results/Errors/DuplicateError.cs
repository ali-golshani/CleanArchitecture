namespace Framework.Results.Errors;

public class DuplicateError : Error
{
    public DuplicateError(string resourceName, object? resourceKey = null)
        : base(ErrorType.Conflict, ErrorMessages.Duplicate(resourceName, resourceKey))
    {
        ResourceName = resourceName;
        ResourceKey = resourceKey;
    }

    public string ResourceName { get; }
    public object? ResourceKey { get; }
}
