using CleanArchitecture.UserManagement.Domain.Repositories;
using CleanArchitecture.UserManagement.Errors;
using Framework.Mediator;
using Framework.Results;
using Framework.Mediator.Middlewares;

namespace CleanArchitecture.UserManagement.Application.Requests.Users.UpdateUser;

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

        user.Update(request.FirstName, request.LastName, request.PhoneNumber);

        return Empty.Value;
    }
}
