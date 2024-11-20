using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;

namespace Framework.Exceptions;

public static class ExceptionExtensions
{
    public static string ToStringDemystified(this Exception exception)
    {
        return System.Diagnostics.ExceptionExtensions.ToStringDemystified(exception);
    }

    public static TException Demystify<TException>(this TException exception)
        where TException : Exception
    {
        return System.Diagnostics.ExceptionExtensions.Demystify(exception);
    }

    public static BaseSystemException TranslateToSystemException(this Exception exp)
    {
        exp = UnwrapAll(exp);

        var result = exp switch
        {
            BaseSystemException systemException => systemException,
            TaskCanceledException or OperationCanceledException => new RequestCanceledException(exp),
            _ => new UnknownException(exp),
        };

        return result.Demystify();
    }

    public static void WaitAndTranslateException(this Task task)
    {
        try
        {
            task.Wait();
        }
        catch (Exception exp)
        {
            if (UnwrapAll(exp, out var innerException))
            {
                ExceptionDispatchInfo.Throw(innerException);
            }

            throw;
        }
    }

    public static T ResultWithTranslateException<T>(this Task<T> task)
    {
        try
        {
            return task.Result;
        }
        catch (Exception exp)
        {
            if (UnwrapAll(exp, out var innerException))
            {
                ExceptionDispatchInfo.Throw(innerException);
            }

            throw;
        }
    }

    public static string StackMessages(this Exception exception)
    {
        var result = new StringBuilder();

        result.AppendLine(exception.Message);

        var exp = exception.InnerException;

        while (exp != null)
        {
            result.AppendLine(exp.Message);
            exp = exp.InnerException;
        }

        return result.ToString();
    }

    public static object? TryInvoke(this MethodInfo method, object? obj, object?[]? parameters)
    {
        try
        {
            return method.Invoke(obj, parameters);
        }
        catch (Exception exp)
        {
            if (UnwrapAll(exp, out var innerException))
            {
                ExceptionDispatchInfo.Throw(innerException);
            }

            throw;
        }
    }

    public static Exception UnwrapAll(this Exception exception)
    {
        UnwrapAll(exception, out var innerException);
        return innerException;
    }

    private static bool UnwrapAll(Exception exception, out Exception innerException)
    {
        ArgumentNullException.ThrowIfNull(exception);

        bool hasChanges = true;

        innerException = exception;

        while (hasChanges)
        {
            hasChanges = false;

            if (innerException is TargetInvocationException && innerException.InnerException != null)
            {
                innerException = innerException.InnerException;
                hasChanges = true;
            }
            else if (innerException is AggregateException { InnerExceptions.Count: 1 } aggregateException)
            {
                innerException = aggregateException.InnerExceptions[0];
                hasChanges = true;
            }
        }

        return hasChanges;
    }
}
