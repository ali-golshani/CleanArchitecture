using Framework.Exceptions;
using Framework.Exceptions.Extensions;

namespace Framework.WebApi.Exceptions;

internal static class ErrorMessages
{
    public static string[] DomainErrorMessages(Exception exp)
    {
        var errors = exp.Errors().ToArray();

        if (errors.Length == 0)
        {
            errors = ["خطای نامشخص"];
        }

        return errors;
    }

    private static IReadOnlyCollection<string> Errors(this Exception exp)
    {
        exp = exp.UnwrapAll();
        return exp switch
        {
            UserFriendlyException friendlyException => friendlyException.Messages,
            BaseSystemException systemException => systemException.Messages,
            _ => [],
        };
    }
}
