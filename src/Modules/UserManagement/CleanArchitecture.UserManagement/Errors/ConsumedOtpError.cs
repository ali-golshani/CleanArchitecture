using CleanArchitecture.UserManagement.Resources;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class ConsumedOtpError()
    : Error(ErrorType.Forbidden, ErrorMessages.ConsumedOtpError);