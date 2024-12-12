namespace Framework.Results.Errors;

public class UnavailableError(string message) : Error(ErrorType.Unavailable, message);
