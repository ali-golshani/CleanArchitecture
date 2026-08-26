using CleanArchitecture.UserManagement.Domain.Repositories;
using CleanArchitecture.UserManagement.Errors;
using CleanArchitecture.UserManagement.Utilities;
using Framework.Mediator;
using Framework.Results;
using Framework.Mediator.Middlewares;

namespace CleanArchitecture.UserManagement.Application.Requests.Users.ResetPassword;

internal sealed class Handler(IUserRepository userRepository) : IRequestHandler<Request, Empty>
{
    private readonly IUserRepository userRepository = userRepository;

    public async Task<Result<Empty>> Handle(RequestContext<Request> context)
    {
        var request = context.Request;
        var cancellationToken = context.CancellationToken;
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
