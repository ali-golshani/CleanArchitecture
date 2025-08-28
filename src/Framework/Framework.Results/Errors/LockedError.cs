namespace Framework.Results.Errors;

public class LockedError(string message, params ErrorSource[] sources) : Error(ErrorType.Locked, message, sources);
