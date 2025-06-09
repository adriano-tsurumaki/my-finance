using System.ComponentModel;
using System.Reflection;

namespace server.Domain.Enums;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var type = value.GetType();
        var name = Enum.GetName(type, value);

        if (name != null)
        {
            var field = type.GetField(name);
            if (field != null)
            {
                var attr = field.GetCustomAttribute<DescriptionAttribute>();
                if (attr != null)
                {
                    return attr.Description;
                }
            }
        }

        return value.ToString(); // Fallback se não tiver [Description]
    }
}
