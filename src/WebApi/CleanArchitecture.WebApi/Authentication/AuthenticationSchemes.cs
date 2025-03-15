namespace CleanArchitecture.WebApi.Authentication;

public static class AuthenticationSchemes
{
    public const string SchemeA = "SchemeA";
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
