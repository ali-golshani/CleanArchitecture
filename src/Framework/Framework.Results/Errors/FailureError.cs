namespace Framework.Results.Errors;

public class FailureError(string message, params ErrorSource[] sources) : Error(ErrorType.Failure, message, sources);
