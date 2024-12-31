using System.Globalization;

namespace Api.Common.Utils;

public static class DateExtensions
{
    public static string ToDateString(this DateTime date)
    {
        if (date == null)
        {
            return string.Empty;
        }

        return date.ToString("dd-MMMMM-yyyy", new CultureInfo("en-GB"));
    }
}
