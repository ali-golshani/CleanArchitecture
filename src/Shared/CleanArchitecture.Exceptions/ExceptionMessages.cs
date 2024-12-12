namespace CleanArchitecture.Exceptions;

internal static class ExceptionMessages
{
    public const string InvalidOperation = "عملیات نامعتبر";
    public const string InvalidRequest = "درخواست نامعتبر";
    public const string NotImplemented = "درخواست مورد نظر توسط سیستم پیاده سازی نشده است";
    public const string NotSupported = "درخواست مورد نظر توسط سیستم پشتیبانی نمی شود";

    public static string OperationAlreadyRunning(string operation)
    {
        return $"عملیات {operation} در حال اجرا و یا پایان یافته است";
    }
}
