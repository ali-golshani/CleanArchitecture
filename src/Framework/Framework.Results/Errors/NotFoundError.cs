namespace Framework.Results.Errors;

public class NotFoundError(string message, params ErrorSource[] sources) : Error(ErrorType.NotFound, message, sources);