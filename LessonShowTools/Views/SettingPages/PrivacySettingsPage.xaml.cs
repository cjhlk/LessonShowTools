using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LessonShowTools.Core.Abstractions.Controls;
using LessonShowTools.Core.Attributes;
using LessonShowTools.Core.Controls;
using LessonShowTools.Core.Enums.SettingsWindow;
using LessonShowTools.Services;
using MaterialDesignThemes.Wpf;

namespace LessonShowTools.Views.SettingPages;

/// <summary>
/// PrivacySettingsPage.xaml 的交互逻辑
/// </summary>
[SettingsPageInfo("privacy", "隐私", PackIconKind.ShieldAccountOutline, PackIconKind.ShieldAccount, true, SettingsPageCategory.Internal)]
//string id, string name, PackIconKind unSelectedIcon, PackIconKind selectedIcon, bool hideDefault, SettingsPageCategory category = SettingsPageCategory.External
public partial class PrivacySettingsPage : SettingsPageBase
{
    public SettingsService SettingsService { get; }

    public PrivacySettingsPage(SettingsService settingsService)
    {
        InitializeComponent();
        DataContext = this;
        SettingsService = settingsService;
        SettingsService.Settings.PropertyChanged += (sender, args) =>
        {
           
        };
    }
}