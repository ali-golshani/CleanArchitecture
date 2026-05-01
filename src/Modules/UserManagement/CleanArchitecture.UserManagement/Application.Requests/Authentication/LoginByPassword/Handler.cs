using CleanArchitecture.UserManagement.Domain;
using CleanArchitecture.UserManagement.Domain.Repositories;
using CleanArchitecture.UserManagement.Domain.Services;
using CleanArchitecture.UserManagement.Domain.Services.Jwt;
using CleanArchitecture.UserManagement.Errors;
using CleanArchitecture.UserManagement.Utilities;
using Framework.Mediator;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Application.Requests.Authentication.LoginByPassword;

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
        var user = await userRepository.Find(request.Username);

        if (user is null)
        {
            return new InvalidPasswordCredentialError();
        }

        if (!user.IsActive)
        {
            return new UserIsLockedOutError();
        }

        var checkPassword = PasswordHasher.Verify(user.Username, user.PasswordHash, request.Password);

        if (!checkPassword)
        {
            return new InvalidPasswordCredentialError();
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
        var hashedRefreshToken = RefreshTokenHasher.Hash(refreshToken);
        var refreshTokenExpirationTime = lifetimeService.RefreshTokenExpirationTime(Shared.SystemClock.Now);

        var session = new AuthenticationSession
        (
            loginMethod: LoginMethod.Password,
            userId: user.Id,
            refreshTokenHash: hashedRefreshToken,
            refreshTokenExpirationTime: refreshTokenExpirationTime
        );

        sessionRepository.Add(session);

        return new Response
        {
            AccessToken = jwtResponse.Token,
            RefreshToken = refreshToken,
            ExpiresIn = jwtResponse.LifetimeSeconds
        };
    }
}