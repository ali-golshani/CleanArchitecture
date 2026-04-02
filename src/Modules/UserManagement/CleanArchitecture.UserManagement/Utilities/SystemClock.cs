namespace CleanArchitecture.UserManagement.Utilities;

internal static class SystemClock
{
    public static DateTime Now => DateTime.Now;
    public static DateTime NextWeek => DateTime.Now.AddDays(7);
}
