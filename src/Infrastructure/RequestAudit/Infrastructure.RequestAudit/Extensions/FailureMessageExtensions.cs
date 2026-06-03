using Framework.Exceptions.Extensions;
using System.Text;

namespace Infrastructure.RequestAudit.Extensions;

internal static class FailureMessageExtensions
{
    public static string FailureMessage(this Error[] errors)
    {
        return TrimResponse(ToString(errors));
    }

    public static string FailureMessage(this Exception exp)
    {
        return TrimResponse(ToString(exp));
    }

    private static string ToString(Error[] errors)
    {
        return string.Join(Environment.NewLine, errors);
    }

    private static string ToString(Exception exp)
    {
        if (exp is BaseSystemException applicationException)
        {
            return
                new StringBuilder()
                .AppendLine(exp.StackMessages())
                .Append(Strings.Stars)
                .AppendLine(applicationException.Properties())
                .Append(Strings.Stars)
                .AppendLine(exp.ToStringDemystified())
                .ToString();
        }
        else
        {
            return
                new StringBuilder()
                .AppendLine(exp.StackMessages())
                .AppendLine(exp.ToStringDemystified())
                .ToString();
        }
    }

    private static string TrimResponse(string response)
    {
        if (response.Length <= Settings.MaxLengthOfAuditTrailResponse)
        {
            return response;
        }

        return response[..Settings.MaxLengthOfAuditTrailResponse];
    }
}
