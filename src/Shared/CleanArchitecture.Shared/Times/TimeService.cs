namespace CleanArchitecture.Shared;

public static class TimeService
{
    public static DateTime Now => DateTime.Now;
    public static DateTime Today => DateTime.Today;
    public static DateTime PreviousWeek => DateTime.Today.AddDays(-7);
}
