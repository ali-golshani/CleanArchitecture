namespace Framework.Results.Errors;

public class UnavailableError(string message, params ErrorSource[] sources) : Error(ErrorType.Unavailable, message, sources);
