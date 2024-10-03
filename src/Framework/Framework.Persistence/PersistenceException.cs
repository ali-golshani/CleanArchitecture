using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Framework.Persistence;

public class PersistenceException : Exceptions.PersistenceException
{
    private const string TransientFailureMessage = "An exception has been raised that is likely due to a transient failure";

    public enum Reasons
    {
        Other = 0,
        DbUpdateException = 1,
        DbUpdateConcurrencyException = 2,
        NotSupportedException = 3,
        ObjectDisposedException = 4,
        InvalidOperationException = 5,

        DuplicatedKeyException = 10,
        UniqueConstraintViolation = 11,
        PermissionWasDenied = 20,
        LoginFailed = 21,

        TransientFailure = 100
    }

    public Reasons Reason { get; }

    public PersistenceException(Reasons reason, Exception innerException)
        : base(innerException)
    {
        Reason = reason;
    }

    public override string Message => $"{base.Message} ({Reason})";

    public static PersistenceException Translate(Exception exception)
    {
        return exception switch
        {
            DbUpdateConcurrencyException _ => new PersistenceException(Reasons.DbUpdateConcurrencyException, exception),
            DbUpdateException dbUpdateException => Translate(dbUpdateException),
            NotSupportedException _ => new PersistenceException(Reasons.NotSupportedException, exception),
            ObjectDisposedException _ => new PersistenceException(Reasons.ObjectDisposedException, exception),
            InvalidOperationException exp when IsTransientFailure(exp) => new PersistenceException(Reasons.TransientFailure, exception),
            InvalidOperationException _ => new PersistenceException(Reasons.InvalidOperationException, exception),
            _ => new PersistenceException(Reasons.Other, exception),
        };
    }

    private static PersistenceException Translate(DbUpdateException exception)
    {
        if (exception.InnerException is SqlException sqlException)
        {
            switch (sqlException.Number)
            {
                case 2601:
                    return new PersistenceException(Reasons.UniqueConstraintViolation, exception);

                case 2627:
                    return new PersistenceException(Reasons.DuplicatedKeyException, exception);

                case 229:
                case 230:
                case 300:
                case 2104:
                case 2557:
                case 2571:
                case 3110:
                case 3505:
                case 4602:
                case 5011:
                case 5812:
                case 6102:
                case 7827:
                case 7983:
                case 9010:
                case 9011:
                case 11423:
                case 12421:
                case 28006:
                case 33428:
                    return new PersistenceException(Reasons.PermissionWasDenied, exception);


                case 18401:
                case 18451:
                case 18452:
                case 18456:
                case 18458:
                case 18459:
                case 18460:
                case 18461:
                case 18462:
                case 18463:
                case 18464:
                case 18465:
                case 18466:
                case 18467:
                case 18468:
                case 18470:
                case 18471:
                case 18486:
                case 18487:
                case 18488:
                    return new PersistenceException(Reasons.LoginFailed, exception);
            }
        }

        return new PersistenceException(Reasons.DbUpdateException, exception);
    }

    private static bool IsTransientFailure(InvalidOperationException exp)
    {
        return exp.Message.Contains(TransientFailureMessage);
    }
}