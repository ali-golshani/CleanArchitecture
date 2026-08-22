using CleanArchitecture.UserManagement.Resources;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class ExpiredOtpError()
    : Error(ErrorCodes.ExpiredOtp, ErrorType.Forbidden, ErrorMessages.ExpiredOtpError);
