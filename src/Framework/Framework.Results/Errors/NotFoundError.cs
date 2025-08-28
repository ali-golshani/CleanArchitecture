using Framework.Results.Resources;

namespace Framework.Results.Errors;

public class NotFoundError : Error
{
    public NotFoundError(string resourceName, object? resourceKey = null)
        : base(ErrorType.NotFound, ErrorMessageBuilder.NotFound(resourceName, resourceKey))
    {
        ResourceName = resourceName;
        ResourceKey = resourceKey;
    }

    public string ResourceName { get; }
    public object? ResourceKey { get; }
}
