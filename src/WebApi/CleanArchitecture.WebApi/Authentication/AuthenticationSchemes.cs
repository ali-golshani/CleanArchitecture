namespace CleanArchitecture.WebApi.Authentication;

public static class AuthenticationSchemes
{
    /// <summary>
    /// By User Management
    /// </summary>
    public const string SchemeA = "SchemeA";

    /// <summary>
    /// http://jwtbuilder.jamiekurtz.com
    /// </summary>
    public const string SchemeB = "SchemeB";

    public const string SchemeAPolicy = "AuthenticationSchemeA";
    public const string SchemeBPolicy = "AuthenticationSchemeB";

    public sealed record Scheme(string Name, string Policy);

    public static readonly Scheme[] Schemes =
    [
        new(SchemeA, SchemeAPolicy),
        new(SchemeB, SchemeBPolicy),
    ];
}
