using System;
using System.Collections.Generic;
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
using LessonShowTools.Core;
using LessonShowTools.Core.Abstractions.Services.Management;
using LessonShowTools.Core.Attributes;
using LessonShowTools.Core.Enums.SettingsWindow;
using LessonShowTools.ViewModels.SettingsPages;

namespace LessonShowTools.Views.SettingPages;

/// <summary>
/// ManagementPolicySettingsPage.xaml 的交互逻辑
/// </summary>
[SettingsPageInfo("management.policy", "集控策略", true, SettingsPageCategory.About)]
public partial class ManagementPolicySettingsPage
{
    public IManagementService ManagementService { get; }
    public ManagementPolicySettingsViewModel ViewModel { get; } = new();

    public ManagementPolicySettingsPage(IManagementService managementService)
    {
        ManagementService = managementService;
        DataContext = this;
        InitializeComponent();
    }

    private void ButtonRestart_OnClick(object sender, RoutedEventArgs e)
    {
        AppBase.Current.Restart();
    }

    private async void ManagementPolicySettingsPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (ManagementService.IsManagementEnabled)
        {
            return;
        }
        var result =
            await ManagementService.AuthorizeByLevel(ManagementService.CredentialConfig
                .EditPolicyAuthorizeLevel);
        if (result)
        {
            ViewModel.IsLocked = false;
        }
    }

    private void ManagementPolicySettingsPage_OnUnloaded(object sender, RoutedEventArgs e)
    {
        ViewModel.IsLocked = true;
    }
}