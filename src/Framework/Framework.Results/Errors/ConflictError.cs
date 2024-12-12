namespace Framework.Results.Errors;

public class ConflictError(string message) : Error(ErrorType.Conflict, message);
