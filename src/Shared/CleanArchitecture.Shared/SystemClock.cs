namespace CleanArchitecture.Shared;

public sealed class SystemClock : IClock
{
    public static DateTime Now => DateTime.Now;
    public static DateOnly Today => DateOnly.FromDateTime(DateTime.Today);

    DateTime IClock.Now => Now;
    DateOnly IClock.Today => Today;
}
