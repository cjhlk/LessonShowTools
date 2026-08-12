using System;
using System.Windows.Controls;

using LessonShowTools.Shared.Interfaces;
using LessonShowTools.Models.AttachedSettings;
using LessonShowTools.Core.Abstractions.Controls;
using LessonShowTools.Core.Attributes;
using LessonShowTools.Core.Enums;
using MaterialDesignThemes.Wpf;

namespace LessonShowTools.Controls.AttachedSettingsControls;

/// <summary>
/// AfterSchoolNotificationAttachedSettingsControl.xaml 的交互逻辑
/// </summary>
[AttachedSettingsUsage(AttachedSettingsTargets.ClassPlan | AttachedSettingsTargets.TimeLayout)]
[AttachedSettingsControlInfo("8FBC3A26-6D20-44DD-B895-B9411E3DDC51", "放学提醒设置", PackIconKind.RunFast)]
public partial class AfterSchoolNotificationAttachedSettingsControl
{
    public AfterSchoolNotificationAttachedSettingsControl()
    {
        InitializeComponent();
    }
}