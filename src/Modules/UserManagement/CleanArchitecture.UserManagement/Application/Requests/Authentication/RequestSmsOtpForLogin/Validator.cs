using FluentValidation;

namespace CleanArchitecture.UserManagement.Application.Requests.Authentication.RequestSmsOtpForLogin;

internal sealed partial class Validator : AbstractValidator<Request>
{
    public Validator()
    {
        RuleFor(x => x.MobileNumber)
            .Matches(Utilities.Validation.MobileNumberRegex());
    }
}
