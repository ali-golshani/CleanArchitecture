using CleanArchitecture.UserManagement.Resources;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class InvalidOtpCredentialError()
    : Error(ErrorCodes.InvalidOtpCredential, ErrorType.Forbidden, ErrorMessages.InvalidOtpCredentialError);
