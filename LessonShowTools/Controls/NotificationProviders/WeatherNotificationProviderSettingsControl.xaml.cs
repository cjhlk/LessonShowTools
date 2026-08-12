using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LessonShowTools.Core.Abstractions.Controls;
using LessonShowTools.Core.Abstractions.Services;
using LessonShowTools.Core.Controls;
using LessonShowTools.Models.NotificationProviderSettings;

namespace LessonShowTools.Controls.NotificationProviders;

/// <summary>
/// WeatherNotificationProviderSettingsControl.xaml 的交互逻辑
/// </summary>
public partial class WeatherNotificationProviderSettingsControl : UserControl
{
    public WeatherNotificationProviderSettings Settings { get; }

    public WeatherNotificationProviderSettingsControl(WeatherNotificationProviderSettings settings)
    {
        Settings = settings;
        InitializeComponent();
    }

    private void ButtonShowAttachedSettingsInfo_OnClick(object sender, RoutedEventArgs e)
    {
        SettingsPageBase.OpenDrawerCommand.Execute(new RootAttachedSettingsDependencyControl(IAttachedSettingsHostService.RegisteredControls.First(x => x.Guid == new Guid("7625DE96-38AA-4B71-B478-3F156DD9458D"))));
    }
}