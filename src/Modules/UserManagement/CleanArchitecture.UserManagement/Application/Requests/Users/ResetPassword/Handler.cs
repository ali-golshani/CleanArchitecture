using CleanArchitecture.UserManagement.Domain.Repositories;
using CleanArchitecture.UserManagement.Errors;
using CleanArchitecture.UserManagement.Utilities;
using Framework.Mediator;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Application.Requests.Users.ResetPassword;

internal sealed class Handler(IUserRepository userRepository) : IRequestHandler<Request, Empty>
{
    private readonly IUserRepository userRepository = userRepository;

    public async Task<Result<Empty>> Handle(Request request, CancellationToken cancellationToken)
    {
        var user = await userRepository.Find(request.UserId);

        if (user is null)
        {
            return new UserNotFoundError();
        }

        var password = $"@{user.Username}";
        var hashedPassword = PasswordHasher.Hash(user.Username, password);

        user.UpdatePassword(hashedPassword);

        return Empty.Value;
    }
}