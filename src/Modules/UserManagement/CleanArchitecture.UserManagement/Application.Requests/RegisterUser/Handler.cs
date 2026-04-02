using CleanArchitecture.UserManagement.Domain;
using CleanArchitecture.UserManagement.Errors;
using CleanArchitecture.UserManagement.Utilities;
using Framework.Mediator;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Application.Requests.RegisterUser;

internal sealed class Handler(IUserRepository userRepository) : IRequestHandler<Request, Empty>
{
    private readonly IUserRepository userRepository = userRepository;

    public async Task<Result<Empty>> Handle(Request request, CancellationToken cancellationToken)
    {
        var user = await userRepository.Find(request.Username);

        if (user is not null)
        {
            return new UsernameAlreadyExistsError();
        }

        var hashedPassword = PasswordHasher.Hash(request.Username, request.Password!);

        user = new User
        (
            firstName: request.FirstName,
            lastName: request.LastName,
            phoneNumber: request.PhoneNumber,
            username: request.Username,
            passwordHash: hashedPassword
        );

        var claim = new UserClaim
        {
            UserId = user.Id,
            ClaimType = UserClaimTypes.Role,
            ClaimValue = request.Role.ToString()
        };

        await userRepository.Add(user);
        await userRepository.Add(claim);

        return Empty.Value;
    }
}