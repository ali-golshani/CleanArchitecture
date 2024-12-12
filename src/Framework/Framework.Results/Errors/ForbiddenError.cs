namespace Framework.Results.Errors;

public class ForbiddenError(string message) : Error(ErrorType.Forbidden, message);
