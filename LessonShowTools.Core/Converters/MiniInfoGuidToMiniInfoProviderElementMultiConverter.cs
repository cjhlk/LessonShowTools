using System.Globalization;
using System.Windows.Data;
using LessonShowTools.Shared;
using LessonShowTools.Shared.Interfaces;

namespace LessonShowTools.Core.Converters;

public class MiniInfoGuidToMiniInfoProviderElementMultiConverter : IMultiValueConverter
{
    public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var guid = (string?)values[0];
        if (guid == null) return null;
        var providers = (ObservableDictionary<string, IMiniInfoProvider>)values[1];
        if (!providers.ContainsKey(guid)) return null;
        return providers[guid].InfoElement;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        return Array.Empty<object>();
    }
}