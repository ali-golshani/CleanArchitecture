using CleanArchitecture.UserManagement.Domain.Repositories;
using CleanArchitecture.UserManagement.Domain.Services;
using CleanArchitecture.UserManagement.Domain.Services.Jwt;
using CleanArchitecture.UserManagement.Utilities;
using Framework.Mediator;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Application.Requests.Authentication.RefreshToken;

internal sealed class Handler(
    JwtService jwtService,
    TokenLifetimeService lifetimeService,
    IUserRepository userRepository,
    ISessionRepository sessionRepository) : IRequestHandler<Request, Response>
{
    private readonly JwtService jwtService = jwtService;
    private readonly TokenLifetimeService lifetimeService = lifetimeService;
    private readonly IUserRepository userRepository = userRepository;
    private readonly ISessionRepository sessionRepository = sessionRepository;

    public async Task<Result<Response>> Handle(Request request, CancellationToken cancellationToken)
    {
        var hashedRefreshToken = RefreshTokenHasher.Hash(request.RefreshToken);
        var session = await sessionRepository.FindByRefreshToken(hashedRefreshToken);

        if (session is null)
        {
            return new Errors.UnauthorizedError();
        }

        if (session.IsLoggedOut ||
            session.RefreshTokenExpirationTime < Shared.SystemClock.Now)
        {
            return new Errors.ExpiredSessionError();
        }

        var user = await userRepository.Find(session.UserId);

        if (user is null)
        {
            return new Errors.UserNotFoundError();
        }

        if (!user.IsActive)
        {
            return new Errors.UserIsLockedOutError();
        }

        var userClaims = await userRepository.GetUserClaims(user.Id);
        var claims = userClaims.Select(x => (x.ClaimType, x.ClaimValue)).ToList();

        var jwtRequest = new JwtRequest
        {
            UserId = user.Id,
            FullName = user.Name,
            Username = user.Username,
            Claims = claims
        };

        var jwtResponse = await jwtService.GenerateToken(jwtRequest);
        var refreshToken = RefreshTokenGenerator.GenerateRefreshToken();
        hashedRefreshToken = RefreshTokenHasher.Hash(refreshToken);

        session.UpdateRefreshToken(hashedRefreshToken, lifetimeService.RefreshTokenExpirationTime(Shared.SystemClock.Now));

        return new Response
        {
            AccessToken = jwtResponse.Token,
            RefreshToken = refreshToken
        };
    }
}