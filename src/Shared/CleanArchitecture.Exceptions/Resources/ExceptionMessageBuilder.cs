namespace CleanArchitecture.Exceptions.Resources;

internal static class ExceptionMessageBuilder
{
    public static string OperationAlreadyRunning(string operation)
    {
        return string.Format(ExceptionMessages.OperationAlreadyRunning, operation);
    }
}
