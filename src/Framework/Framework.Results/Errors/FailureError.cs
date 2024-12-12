namespace Framework.Results.Errors;

public class FailureError(string message) : Error(ErrorType.Failure, message);
