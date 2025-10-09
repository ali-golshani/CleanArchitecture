namespace CleanArchitecture.Shared;

public sealed class SystemDateTime : IDateTime
{
    public static DateTime Now => DateTime.Now;
    public static DateOnly Today => DateOnly.FromDateTime(DateTime.Today);

    DateTime IDateTime.Now => Now;
    DateOnly IDateTime.Today => Today;
}
