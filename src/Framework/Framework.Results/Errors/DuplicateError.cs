using Framework.Results.Resources;

namespace Framework.Results.Errors;

public class DuplicateError : Error
{
    public DuplicateError(string resourceName, object? resourceKey = null)
        : base(ErrorType.Conflict, ErrorMessageBuilder.Duplicate(resourceName, resourceKey))
    {
        ResourceName = resourceName;
        ResourceKey = resourceKey;
    }

    public string ResourceName { get; }
    public object? ResourceKey { get; }
}
