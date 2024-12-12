using Framework.Results;

namespace CleanArchitecture.Errors;

public class DuplicateError : Error
{
    public DuplicateError(string resourceName, object? resourceKey = null)
        : base(ErrorType.Conflict, ErrorMessage(resourceName, resourceKey))
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
            return $"{resourceName} با شناسه درخواستی وجود دارد";
        }
        else
        {
            return $"{resourceName} با شناسه {resourceKey} وجود دارد";
        }
    }
}
