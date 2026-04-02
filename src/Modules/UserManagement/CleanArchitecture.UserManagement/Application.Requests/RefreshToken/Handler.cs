using CleanArchitecture.UserManagement.Domain;
using CleanArchitecture.UserManagement.Domain.Services;
using CleanArchitecture.UserManagement.Utilities;
using Framework.Mediator;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Application.Requests.RefreshToken;

internal sealed class Handler(
    JwtService jwtService,
    IUserRepository userRepository) : IRequestHandler<Request, Response>
{
    private readonly JwtService jwtService = jwtService;
    private readonly IUserRepository userRepository = userRepository;

    public async Task<Result<Response>> Handle(Request request, CancellationToken cancellationToken)
    {
        var principal = jwtService.GetPrincipalFromExpiredToken(request.Token!);

        if (principal?.Identity?.Name is null)
        {
            return new Errors.UnauthorizedError();
        }

        var user = await userRepository.Find(principal.Identity.Name);

        if (user is null ||
            user.RefreshToken != request.RefreshToken ||
            user.RefreshTokenExpiry < SystemClock.Now)
        {
            return new Errors.UnauthorizedError();
        }

        var userClaims = await userRepository.UserClaims(user.Id);
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

        user.UpdateRefreshToken(refreshToken, SystemClock.NextWeek);

        return new Response
        {
            Token = jwtResponse.Token,
            RefreshToken = refreshToken
        };
    }
}