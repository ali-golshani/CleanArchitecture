namespace Framework.Results.Errors;

public class LockedError(string message) : Error(ErrorType.Locked, message);
