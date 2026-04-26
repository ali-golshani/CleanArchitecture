using CleanArchitecture.Authorization.Claims;
using CleanArchitecture.UserManagement.Options;
using CleanArchitecture.UserManagement.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CleanArchitecture.UserManagement.Domain.Services.Jwt;

internal sealed class JwtService(
    IOptions<JwtOptions> jwtOptions,
    TokenLifetimeService lifetimeService)
{
    private readonly JwtOptions jwtOptions = jwtOptions.Value;
    private readonly TokenLifetimeService lifetimeService = lifetimeService;

    private SecurityKey IssuerSigningKey => Security.IssuerSigningKey(jwtOptions.SecretKey);

    public async Task<JwtResponse> GenerateToken(JwtRequest request)
    {
        var time = Shared.SystemClock.Now;

        var signingCredentials = new SigningCredentials(
            IssuerSigningKey,
            SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            issuer: jwtOptions.Issuer,
            audience: jwtOptions.Audience,
            claims: Claims(request),
            notBefore: time,
            expires: lifetimeService.TokenExpirationTime(time),
            signingCredentials: signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return await Task.FromResult(new JwtResponse
        {
            LifetimeSeconds = lifetimeService.TokenLifetimeSeconds,
            Token = token,
        });
    }

    private static List<Claim> Claims(JwtRequest request)
    {
        var result = new List<Claim>
        {
            new(UserClaimTypes.UserId, request.UserId.ToString()),
            new(UserClaimTypes.Username, request.Username),
            new(UserClaimTypes.DisplayName, request.FullName),
        };

        foreach (var (ClaimType, ClaimValue) in request.Claims)
        {
            result.Add(new(ClaimType, ClaimValue));
        }

        return result;
    }
}