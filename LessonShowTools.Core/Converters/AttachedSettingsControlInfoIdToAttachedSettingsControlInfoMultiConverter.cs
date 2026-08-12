using System.Globalization;
using System.Windows.Data;
using LessonShowTools.Core.Attributes;

namespace LessonShowTools.Core.Converters;

public class
    AttachedSettingsControlInfoIdToAttachedSettingsControlInfoMultiConverter : IMultiValueConverter
{
    public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 2)
        {
            return null;
        }

        if (values[0] is not ICollection<AttachedSettingsControlInfo> c || values[1] is not string id)
        {
            return null;
        }

        return c.FirstOrDefault(x => x.Guid.ToString() == id);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        return Array.Empty<object>();
    }
}