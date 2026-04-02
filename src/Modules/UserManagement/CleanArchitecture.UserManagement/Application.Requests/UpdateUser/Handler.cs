using CleanArchitecture.UserManagement.Domain;
using CleanArchitecture.UserManagement.Errors;
using Framework.Mediator;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Application.Requests.UpdateUser;

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

        user.Update(request.FirstName, request.LastName, request.PhoneNumber);

        return Empty.Value;
    }
}