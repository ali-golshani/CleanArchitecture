namespace Framework.Results.Errors;

public class ConflictError(string message, params ErrorSource[] sources) : Error(ErrorType.Conflict, message, sources);
