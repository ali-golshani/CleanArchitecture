namespace CleanArchitecture.WebApi.Authentication;

public static class AuthenticationSchemes
{
    public const string SchemaA = "SchemaA";
    public const string SchemaB = "SchemaB";

    public const string SchemaAPolicy = "AuthenticationSchemeA";
    public const string SchemaBPolicy = "AuthenticationSchemeB";

    public sealed record Schema(string Name, string Policy);

    public static readonly Schema[] Schemas =
    [
        new(SchemaA, SchemaAPolicy),
        new(SchemaB, SchemaBPolicy),
    ];
}
