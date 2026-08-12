using CommunityToolkit.Mvvm.ComponentModel;

namespace LessonShowTools.Models.Rules;

public partial class CurrentWeatherRuleSettings : ObservableObject
{
    [ObservableProperty] private int _weatherId = 0;
}