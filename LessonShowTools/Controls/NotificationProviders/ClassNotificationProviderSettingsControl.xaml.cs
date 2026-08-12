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
/// ClassNotificationProviderSettingsControl.xaml 的交互逻辑
/// </summary>
public partial class ClassNotificationProviderSettingsControl : UserControl
{
    public ClassNotificationSettings Settings
    {
        get;
        set;
    }

    public ClassNotificationProviderSettingsControl(ClassNotificationSettings settings)
    {
        Settings = settings;
        InitializeComponent();
    }

    private void ButtonShowAttachedSettingsInfo_OnClick(object sender, RoutedEventArgs e)
    {
        SettingsPageBase.OpenDrawerCommand.Execute(new RootAttachedSettingsDependencyControl(IAttachedSettingsHostService.RegisteredControls.First(x => x.Guid == new Guid("08F0D9C3-C770-4093-A3D0-02F3D90C24BC"))));
    }
}