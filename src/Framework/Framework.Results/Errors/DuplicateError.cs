namespace Framework.Results.Errors;

public class DuplicateError(string message, params ErrorSource[] sources) : Error(ErrorType.Conflict, message, sources);
