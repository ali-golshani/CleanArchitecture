using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CleanArchitecture.UserManagement.Utilities;

internal static class Security
{
    public static SecurityKey IssuerSigningKey(string secretKey)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
    }
}