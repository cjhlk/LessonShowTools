using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
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
using LessonShowTools.Core.Abstractions.Services;
using LessonShowTools.Core.Attributes;
using LessonShowTools.Core.Enums.SettingsWindow;
using LessonShowTools.Core.Helpers;
using LessonShowTools.Core.Models.Plugin;
using LessonShowTools.Services;
using LessonShowTools.ViewModels.SettingsPages;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Sentry;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using CommonDialog = LessonShowTools.Core.Controls.CommonDialog.CommonDialog;
using Path = System.IO.Path;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace LessonShowTools.Views.SettingPages;

/// <summary>
/// PluginsSettingsPage.xaml 的交互逻辑
/// </summary>
///
[SettingsPageInfo("LessonShowTools.plugins", "插件", PackIconKind.ToyBrickOutline, PackIconKind.ToyBrick, true, SettingsPageCategory.Internal)]
public partial class PluginsSettingsPage : SettingsPageBase
{
    public PluginsSettingsPageViewModel ViewModel { get; } = new();

    public IPluginService PluginService { get; }
    public IPluginMarketService PluginMarketService { get; }
    public SettingsService SettingsService { get; }

    private CancellationTokenSource DocumentLoadingCancellationTokenSource { get; set; } = new();

    public PluginsSettingsPage(IPluginService pluginService, IPluginMarketService pluginMarketService, SettingsService settingsService)
    {
        InitializeComponent();
        DataContext = this;
        PluginService = pluginService;
        PluginMarketService = pluginMarketService;
        SettingsService = settingsService;
        ViewModel.PropertyChanged += ViewModelOnPropertyChanged;
        PluginMarketService.RestartRequested += (sender, args) => RequestRestart();
        if (DateTime.Now - SettingsService.Settings.LastRefreshPluginSourceTime >= TimeSpan.FromDays(7))
        {
            _ = PluginMarketService.RefreshPluginSourceAsync();
        }
    }

    private async Task UpdateReadmeDocument()
    {
     
    }

    private async void ViewModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(ViewModel.SelectedPluginInfo):
                await UpdateReadmeDocument();
                break;
        }
    }

    private void UIElement_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (!e.Handled)
        {
            // ListView拦截鼠标滚轮事件
            e.Handled = true;

            // 激发一个鼠标滚轮事件，冒泡给外层ListView接收到
            var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            eventArg.RoutedEvent = UIElement.MouseWheelEvent;
            eventArg.Source = sender;
            var parent = ((System.Windows.Controls.Control)sender).Parent as UIElement;
            if (parent != null)
            {
                parent.RaiseEvent(eventArg);
            }
        }
    }

    private void ButtonUninstall_OnClick(object sender, RoutedEventArgs e)
    {
        if (ViewModel.SelectedPluginInfo == null)
            return;
        ViewModel.SelectedPluginInfo.IsUninstalling = true;
        RequestRestart();
    }

    private void ButtonUndoUninstall_OnClick(object sender, RoutedEventArgs e)
    {
        if (ViewModel.SelectedPluginInfo == null)
            return;
        ViewModel.SelectedPluginInfo.IsUninstalling = false;
    }

    private async void MenuItemPackPlugin_OnClick(object sender, RoutedEventArgs e)
    {
        CommonDialog.ShowError($"error");
    }

    private void MenuItemOpenPluginFolder_OnClick(object sender, RoutedEventArgs e)
    {
        if (ViewModel.SelectedPluginInfo == null)
            return;
        Process.Start(new ProcessStartInfo()
        {
            FileName = ViewModel.SelectedPluginInfo.PluginFolderPath,
            UseShellExecute = true
        });
    }

    private void ButtonInstallFromLocal_OnClick(object sender, RoutedEventArgs e)
    {

            CommonDialog.ShowError($"error");

    }

    private void MenuItemOpenPluginConfigFolder_OnClick(object sender, RoutedEventArgs e)
    {
        CommonDialog.ShowError($"error");
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.IsPluginOperationsPopupOpened = false;
    }

    private async void ButtonBaseRefreshPlugins_OnClick(object sender, RoutedEventArgs e)
    {
        await PluginMarketService.RefreshPluginSourceAsync();
        if (FindResource("PluginSource") is CollectionViewSource source)
        {
            source.View.Refresh();
        }
    }

    private void ButtonInstallPlugin_OnClick(object sender, RoutedEventArgs e)
    {
        if (ViewModel.SelectedPluginInfo == null)
            return;
        PluginMarketService.RequestDownloadPlugin(ViewModel.SelectedPluginInfo.Manifest.Id);
    }

    private void MenuItemReloadFromCache_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.IsPluginMarketOperationsPopupOpened = false;
        PluginMarketService.LoadPluginSource();
    }

    private void MenuItemManagePluginSources_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.IsPluginMarketOperationsPopupOpened = false;
        OpenDrawer("PluginSourceManageDrawer");
    }

    private void MenuItemOpenPluginsFolder_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.IsPluginMarketOperationsPopupOpened = false;
        Process.Start(new ProcessStartInfo()
        {
            FileName = Services.PluginService.PluginsRootPath,
            UseShellExecute = true
        });
    }

    private void ButtonBase2_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.IsPluginMarketOperationsPopupOpened = false;
    }

    private void ButtonAddPluginSource_OnClick(object sender, RoutedEventArgs e)
    {
        
    }

    private void ButtonRemovePluginSource_OnClick(object sender, RoutedEventArgs e)
    {
        if (ViewModel.SelectedPluginIndexInfo == null)
            return;
        SettingsService.Settings.PluginIndexes.Remove(ViewModel.SelectedPluginIndexInfo);
    }

    private void ListBoxCategory_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      
    }

    private void PluginSource_OnFilter(object sender, FilterEventArgs e)
    {
        if (e.Item is not KeyValuePair<string, PluginInfo> kvp) 
            return;
        var info = kvp.Value;
        if (!info.IsLocal && ViewModel.PluginCategoryIndex == 1)
        {
            e.Accepted = false;
            return;
        }
        if (!info.IsAvailableOnMarket && ViewModel.PluginCategoryIndex == 0)
        {
            e.Accepted = false;
            return;
        }
        
        var filter = ViewModel.PluginFilterText;
        if (string.IsNullOrWhiteSpace(filter))
            return;
        e.Accepted = info.Manifest.Id.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                     info.Manifest.Name.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                     info.Manifest.Description.Contains(filter, StringComparison.OrdinalIgnoreCase);
    }

    private void TextBoxFilter_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter) return;
        Focus();
        if (FindResource("PluginSource") is CollectionViewSource source)
        {
            source.View.Refresh();
        }
    }

    private void ButtonRestart_OnClick(object sender, RoutedEventArgs e)
    {
        RequestRestart();
    }

    private void ButtonAgreePluginNotice_OnClick(object sender, RoutedEventArgs e)
    {
        SettingsService.Settings.IsPluginMarketWarningVisible = false;
    }

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ViewModel.SelectedPluginInfo == null)
        {
            ViewModel.IsDetailsShown = false;
            return;
        }

        ViewModel.IsDetailsShown = true;
    }
}