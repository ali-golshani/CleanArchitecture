using Framework.Mediator;
using Infrastructure.RequestAudit.Domain;

namespace Infrastructure.RequestAudit.Extensions;

internal static class AuditTrailExtensions
{
    public static AuditTrail LogEntry(
        this Request request,
        Actor? actor,
        string? domain = null,
        bool? shouldLog = null)
    {
        return new AuditTrail
        (
            correlationId: request.CorrelationId,
            actor: actor?.ToString() ?? Strings.Question,
            domain: domain ?? nameof(CleanArchitecture),
            requestType: request.GetType().FullName ?? Strings.Question,
            request: request.SerializeToJson(),
            parameters: RequestParametersResolver.RequestParameters(request),
            shouldLog: shouldLog ?? request.ShouldLog
        );
    }

    public static void Responsed<TResponse>(
        this AuditTrail logEntry,
        TimeSpan responseTime,
        Request request,
        TResponse response)
    {
        logEntry.Succeed
        (
            responseTime: responseTime,
            response: response.ResponseString(),
            parameters: RequestParametersResolver.ResponseParameters(response)
        );
    }

    private static string? ResponseString<TResponse>(this TResponse response)
    {
        if (response is null)
        {
            return null;
        }

        if (response is IAuditableResponse auditableResponse)
        {
            return auditableResponse.AuditTrailString();
        }

        if (response.GetType().IsPrimitive)
        {
            return response.ToString();
        }

        switch (Type.GetTypeCode(response.GetType()))
        {
            case TypeCode.Boolean:
            case TypeCode.Char:
            case TypeCode.SByte:
            case TypeCode.Byte:
            case TypeCode.Int16:
            case TypeCode.UInt16:
            case TypeCode.Int32:
            case TypeCode.UInt32:
            case TypeCode.Int64:
            case TypeCode.UInt64:
            case TypeCode.Single:
            case TypeCode.Double:
            case TypeCode.Decimal:
            case TypeCode.DateTime:
            case TypeCode.String:
                return response.ToString();

            case TypeCode.Empty:
            case TypeCode.Object:
            case TypeCode.DBNull:
            default:
                return null;
        }
    }
}
