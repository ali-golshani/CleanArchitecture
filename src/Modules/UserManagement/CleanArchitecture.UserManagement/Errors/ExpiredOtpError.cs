using CleanArchitecture.UserManagement.Resources;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class ExpiredOtpError()
    : Framework.Results.Errors.ForbiddenError(ErrorMessages.ExpiredOtpError);