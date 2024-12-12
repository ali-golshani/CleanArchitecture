namespace Framework.Results;

internal static class ErrorMessages
{
    public const string AccessDenied = "دسترسی غیر مجاز";
    public const string Unauthorized = "کاربر درخواست دهنده احراز هویت نشده است";
    public const string InvalidRequest = "درخواست نامعتبر";
    public const string NotSupported = "درخواست مورد نظر توسط سیستم پشتیبانی نمی شود";
    public const string OperationCanceled = "عملیات لغو گردید";
    public const string UnknownError = "خطای نامشخص";

    public static string NotFound(string resourceName, object? resourceKey)
    {
        if (resourceKey is null)
        {
            return $"{resourceName} مورد نظر یافت نشد";
        }
        else
        {
            return $"{resourceName} با شناسه {resourceKey} یافت نشد";
        }
    }
}
