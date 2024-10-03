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
        exp = ResolveAggregateException(exp);

        var result =
            exp is BaseSystemException systemException ?
            systemException :
            new UnknownException(exp);

        return result.Demystify();
    }

    public static void WaitAndTranslateException(this Task task)
    {
        try
        {
            task.Wait();
        }
        catch (AggregateException aggregateException)
        {
            if (aggregateException.InnerExceptions.Count == 1)
            {
                ExceptionDispatchInfo.Throw(aggregateException.InnerException!);
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
        catch (AggregateException aggregateException)
        {
            if (aggregateException.InnerExceptions.Count == 1)
            {
                ExceptionDispatchInfo.Throw(aggregateException.InnerException!);
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
        catch (TargetInvocationException exp)
        {
            if (exp.InnerException is not null)
            {
                ExceptionDispatchInfo.Throw(exp.InnerException);
            }

            throw;
        }
    }

    private static Exception ResolveAggregateException(Exception exp)
    {
        if (exp is AggregateException aggregateException &&
            aggregateException.InnerExceptions.Count == 1)
        {
            return aggregateException.InnerException!;
        }

        return exp;
    }
}
