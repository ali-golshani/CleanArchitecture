using CleanArchitecture.Audit.Domain;

namespace CleanArchitecture.Audit;

internal static class AuditTrailExtensions
{
    public static CommandAuditTrail LogEntry(
        this Command command,
        Actor actor,
        string? domain = null)
    {
        return new CommandAuditTrail
        (
            correlationId: command.CorrelationId,
            actor: ActorSerializer.Serialize(actor),
            domain: domain ?? nameof(CleanArchitecture),
            command: command.GetType().FullName ?? Strings.Question,
            request: command.SerializeToJson(),
            parameters: RequestParametersResolver.RequestParameters(command)
        );
    }

    public static QueryAuditTrail LogEntry(
        this Query query,
        Actor actor,
        string? domain = null,
        bool? shouldLog = null)
    {
        return new QueryAuditTrail
        (
            correlationId: query.CorrelationId,
            actor: ActorSerializer.Serialize(actor),
            domain: domain ?? nameof(CleanArchitecture),
            query: query.GetType().FullName ?? Strings.Question,
            request: query.SerializeToJson(),
            parameters: RequestParametersResolver.RequestParameters(query),
            shouldLog: shouldLog ?? query.ShouldLog
        );
    }

    public static void Responsed<TResponse>(
        this CommandAuditTrail logEntry,
        TimeSpan responseTime,
        Command command,
        TResponse response)
    {
        logEntry.Succeed
        (
            responseTime: responseTime,
            response: ResponseString(response),
            parameters: RequestParametersResolver.ResponseParameters(response)
        );
    }

    private static string? ResponseString<TResponse>(this TResponse response)
    {
        if (response is null)
        {
            return null;
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
