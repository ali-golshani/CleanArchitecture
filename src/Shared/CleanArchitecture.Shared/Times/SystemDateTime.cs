namespace CleanArchitecture.Shared;

public class SystemDateTime : IDateTime
{
    public static DateTime Now => DateTime.Now;
    public static DateOnly Today => DateOnly.FromDateTime(DateTime.Today);
    public DateOnly PreviousWeek => Today.AddDays(-7);

    DateTime IDateTime.Now => Now;
    DateOnly IDateTime.Today => Today;
}
