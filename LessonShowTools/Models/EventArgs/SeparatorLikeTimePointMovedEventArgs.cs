using System.Windows;
using LessonShowTools.Controls;
using LessonShowTools.Shared.Models.Profile;

namespace LessonShowTools.Models.EventArgs;

public class SeparatorLikeTimePointMovedEventArgs(TimeLayoutItem item) : RoutedEventArgs(TimeLineListItemSeparatorAdornerControl.SeparatorLikeTimePointMovedEvent)
{
    public TimeLayoutItem Item { get; } = item;
}