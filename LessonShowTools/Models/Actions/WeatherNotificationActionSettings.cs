using CommunityToolkit.Mvvm.ComponentModel;

namespace LessonShowTools.Models.Actions;

public partial class WeatherNotificationActionSettings : ObservableObject
{
    [ObservableProperty] private int _notificationKind = 0;
}