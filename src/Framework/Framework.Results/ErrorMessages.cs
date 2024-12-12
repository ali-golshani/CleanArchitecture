namespace Framework.Results;

internal static class ErrorMessages
{
    public const string Forbidden = "دسترسی غیر مجاز";
    public const string Unauthorized = "کاربر درخواست دهنده احراز هویت نشده است";
    public const string OperationCanceled = "عملیات لغو گردید";
    public const string Unexpected = "خطای نامشخص";
    public const string NotSupported = "درخواست مورد نظر توسط سیستم پشتیبانی نمی شود";
    public const string NotImplemented = "درخواست مورد نظر توسط سیستم پیاده سازی نشده است";
    public const string Timeout = "درخواست به دلیل اتمام زمان لغو گردید";
    public const string InvalidRequest = "درخواست نامعتبر";

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

    public static string Duplicate(string resourceName, object? resourceKey)
    {
        if (resourceKey is null)
        {
            return $"{resourceName} با شناسه درخواستی وجود دارد";
        }
        else
        {
            return $"{resourceName} با شناسه {resourceKey} وجود دارد";
        }
    }
}
