using CleanArchitecture.UserManagement.Resources;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class InvalidOtpError()
    : Error(ErrorType.Forbidden, ErrorMessages.InvalidOtpError);