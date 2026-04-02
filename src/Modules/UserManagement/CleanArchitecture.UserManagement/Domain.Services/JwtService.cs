using CleanArchitecture.UserManagement.Options;
using CleanArchitecture.UserManagement.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CleanArchitecture.UserManagement.Domain.Services;

internal sealed class JwtService(IOptions<JwtOptions> jwtOptions)
{
    private readonly JwtOptions jwtOptions = jwtOptions.Value;
    private SecurityKey IssuerSigningKey => Security.IssuerSigningKey(jwtOptions.SecretKey);

    public async Task<JwtResponse> GenerateToken(JwtRequest request)
    {
        var time = SystemClock.Now;

        var signingCredentials = new SigningCredentials(
            IssuerSigningKey,
            SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            issuer: jwtOptions.Issuer,
            audience: jwtOptions.Audience,
            claims: Claims(request),
            notBefore: time,
            expires: time.AddSeconds(Settings.TokenExpirySeconds),
            signingCredentials: signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return await Task.FromResult(new JwtResponse
        {
            Expiry = Settings.TokenExpirySeconds,
            Token = token,
        });
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var validation = new TokenValidationParameters
        {
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = IssuerSigningKey,
            ValidateLifetime = false
        };

        try
        {
            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
        }
        catch (SecurityTokenArgumentException)
        {
            return null;
        }
        catch (SecurityTokenException)
        {
            return null;
        }
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