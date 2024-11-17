namespace CleanArchitecture.WebApi.Authentication;

public static class AuthenticationSchemes
{
    public const string Default = "Default";
    public const string InternalServices = "InternalServices";

    public const string DefaultPolicy = "DefaultAuthenticationScheme";
    public const string InternalServicesPolicy = "InternalServicesAuthenticationScheme";

    public sealed record Schema(string Name, string Policy);

    public static readonly Schema[] Schemas =
    [
        new(Default, DefaultPolicy),
        new(InternalServices, InternalServicesPolicy),
    ];
}
