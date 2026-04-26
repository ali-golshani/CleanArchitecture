using CleanArchitecture.UserManagement.Resources;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class InvalidOtpError()
    : Framework.Results.Errors.ForbiddenError(ErrorMessages.InvalidOtpError);