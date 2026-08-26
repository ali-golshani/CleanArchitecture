using CleanArchitecture.UserManagement.Domain.Repositories;
using CleanArchitecture.UserManagement.Utilities;
using Framework.Mediator;
using Framework.Results;
using Framework.Mediator.Middlewares;

namespace CleanArchitecture.UserManagement.Application.Requests.Authentication.Logout;

internal sealed class Handler(ISessionRepository sessionRepository) : IRequestHandler<Request, Empty>
{
    private readonly ISessionRepository sessionRepository = sessionRepository;

    public async Task<Result<Empty>> Handle(RequestContext<Request> context)
    {
        var request = context.Request;
        var cancellationToken = context.CancellationToken;
        var hashedRefreshToken = RefreshTokenHasher.Hash(request.RefreshToken);
        var session = await sessionRepository.FindByRefreshToken(hashedRefreshToken);

        if (session is null || session.IsLoggedOut)
        {
            return Empty.Value;
        }

        session.Logout();

        return Empty.Value;
    }
}
