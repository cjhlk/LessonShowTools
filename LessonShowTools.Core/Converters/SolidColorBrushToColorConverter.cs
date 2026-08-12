using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LessonShowTools.Core.Converters;

public class SolidColorBrushToColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var v = value as SolidColorBrush;
        return v?.Color ?? Color.FromRgb(0, 0, 0);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}