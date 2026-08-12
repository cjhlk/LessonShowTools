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
using LessonShowTools.Core.Abstractions.Services.Management;
using LessonShowTools.Core.Attributes;
using LessonShowTools.Core.Enums.SettingsWindow;
using LessonShowTools.ViewModels.SettingsPages;

namespace LessonShowTools.Views.SettingPages;

/// <summary>
/// ManagementCredentialsSettingsPage.xaml 的交互逻辑
/// </summary>
[SettingsPageInfo("management.credentials", "集控凭据设置", true, SettingsPageCategory.About)]
public partial class ManagementCredentialsSettingsPage
{
    public ManagementCredentialsSettingsViewModel ViewModel { get; } = new();

    public IManagementService ManagementService { get; }

    public ManagementCredentialsSettingsPage(IManagementService managementService)
    {
        ManagementService = managementService;
        DataContext = this;
        InitializeComponent();
    }

    private async void ManagementCredentialsSettingsPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (ManagementService.IsManagementEnabled)
        {
            return;
        }
        var result =
            await ManagementService.AuthorizeByLevel(ManagementService.CredentialConfig
                .EditAuthorizeSettingsAuthorizeLevel);
        if (result)
        {
            ViewModel.IsLocked = false;
        }
    }

    private void ManagementCredentialsSettingsPage_OnUnloaded(object sender, RoutedEventArgs e)
    {
        ViewModel.IsLocked = true;
    }
}