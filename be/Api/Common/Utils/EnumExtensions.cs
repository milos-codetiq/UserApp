using System.ComponentModel;

namespace Api.Common.Utils;

public static class EnumExtensions
{
    public static string GetDescription(this Enum source)
    {
        if (source == null)
            return string.Empty;

        var type = source.GetType();

        var memInfo = type.GetMember(source.ToString());

        if (memInfo.Length > 0)
        {

            var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs.Length > 0)
            {
                return ((DescriptionAttribute)attrs[0]).Description;
            }
        }

        return source.ToString();
    }
}
