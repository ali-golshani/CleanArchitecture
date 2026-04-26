using CleanArchitecture.UserManagement.Resources;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class ConsumedOtpError()
    : Framework.Results.Errors.ForbiddenError(ErrorMessages.ConsumedOtpError);