using System;
using System.Windows.Controls;
using LessonShowTools.Core.Attributes;
using LessonShowTools.Core.Enums;
using LessonShowTools.Shared.Interfaces;
using LessonShowTools.Models.AttachedSettings;
using MaterialDesignThemes.Wpf;

namespace LessonShowTools.Controls.AttachedSettingsControls;

/// <summary>
/// WeatherNotificationAttachedSettingsControl.xaml 的交互逻辑
/// </summary>
[AttachedSettingsUsage(AttachedSettingsTargets.TimePoint)]
[AttachedSettingsControlInfo("7625DE96-38AA-4B71-B478-3F156DD9458D", "天气提醒设置", PackIconKind.WeatherCloudy, false)]
public partial class WeatherNotificationAttachedSettingsControl
{
    public WeatherNotificationAttachedSettingsControl()
    {
        InitializeComponent();
    }
}