using CleanArchitecture.UserManagement.Domain;
using CleanArchitecture.UserManagement.Errors;
using CleanArchitecture.UserManagement.Utilities;
using Framework.Mediator;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Application.Requests.ChangePassword;

internal sealed class Handler(IUserRepository userRepository) : IRequestHandler<Request, Empty>
{
    private readonly IUserRepository userRepository = userRepository;

    public async Task<Result<Empty>> Handle(Request request, CancellationToken cancellationToken)
    {
        var user = await userRepository.Find(request.Username);

        if (user is null)
        {
            return new UserNotFoundError();
        }

        var oldHashedPassword = PasswordHasher.Hash(user.Username, request.OldPassword);

        if (oldHashedPassword != user.PasswordHash)
        {
            return new InvalidCredentialError();
        }

        var hashedPassword = PasswordHasher.Hash(user.Username, request.NewPassword);

        user.UpdatePassword(hashedPassword);

        return Empty.Value;
    }
}