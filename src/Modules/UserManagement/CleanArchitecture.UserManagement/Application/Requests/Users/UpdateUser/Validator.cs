using FluentValidation;

namespace CleanArchitecture.UserManagement.Application.Requests.Users.UpdateUser;

internal sealed partial class Validator : AbstractValidator<Request>
{
    public Validator()
    {
        RuleFor(x => x.FirstName)
            .MinimumLength(2)
            .MaximumLength(128);

        RuleFor(x => x.LastName)
            .MinimumLength(2)
            .MaximumLength(128);

    }
}
