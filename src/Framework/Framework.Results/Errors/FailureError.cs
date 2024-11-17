namespace Framework.Results;

public class FailureError(string message) : Error(ErrorType.Failure, message)
{
}
