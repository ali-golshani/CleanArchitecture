using CleanArchitecture.UserManagement.Resources;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class InvalidOtpCredentialError()
    : Framework.Results.Errors.ForbiddenError(ErrorMessages.InvalidOtpCredentialError);