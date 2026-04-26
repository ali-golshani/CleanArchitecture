using System.Text.RegularExpressions;

namespace Framework.Utilities;

public static partial class PersianDateConverter
{
    public static string? ToPersianDate(this DateTime? date)
    {
        return
            date == null
            ?
            null
            :
            ToPersianDate(date.Value);
    }

    public static string ToPersianDate(this DateTime date)
    {
        try
        {
            var persianCalendar = new System.Globalization.PersianCalendar();

            return
                persianCalendar.GetYear(date).ToString("0000")
                +
                "/"
                +
                persianCalendar.GetMonth(date).ToString("00")
                +
                "/"
                +
                persianCalendar.GetDayOfMonth(date).ToString("00")
                ;
        }
        catch
        {
            return date.ToShortDateString();
        }
    }

    public static string? ToPersianDateTime(this DateTime? date)
    {
        return
            date == null
            ?
            null
            :
            ToPersianDateTime(date.Value);
    }

    public static string ToPersianDateTime(this DateTime date)
    {
        var persianDate = ToPersianDate(date);
        var persianTime = date.ToString("HH:mm:ss");

        return $"{persianDate} {persianTime}";
    }

    public static DateTime? FromPersianDate(string? persianDate)
    {
        if (string.IsNullOrEmpty(persianDate))
        {
            return null;
        }

        try
        {
            if (PersianDateRegex().IsMatch(persianDate))
            {
                var groups = PersianDateRegex().Match(persianDate).Groups;
                int year = Convert.ToInt32(groups["year"].Value);
                int month = Convert.ToInt32(groups["month"].Value);
                int day = Convert.ToInt32(groups["day"].Value);

                var calendar = new System.Globalization.PersianCalendar();

                return calendar.ToDateTime(year, month, day, 0, 0, 0, 0);
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }

    public static int? PersianYear(string? persianDate)
    {
        if (string.IsNullOrEmpty(persianDate))
        {
            return null;
        }

        try
        {
            if (PersianDateRegex().IsMatch(persianDate))
            {
                var groups = PersianDateRegex().Match(persianDate).Groups;
                int year = Convert.ToInt32(groups["year"].Value);

                return year;
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }

    public static int? PersianYear(DateTime date)
    {
        try
        {
            var persianCalendar = new System.Globalization.PersianCalendar();
            return persianCalendar.GetYear(date);
        }
        catch
        {
            return null;
        }
    }

    [GeneratedRegex(@"^(?<year>\d{4})(?<space>[-,/])(?<month>\d{1,2})(\k<space>)(?<day>\d{1,2})$")]
    internal static partial Regex PersianDateRegex();
}
