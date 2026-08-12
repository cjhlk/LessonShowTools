using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using LessonShowTools.Core.Abstractions.Controls;
using LessonShowTools.Core.Attributes;
using LessonShowTools.Core.Enums.SettingsWindow;
using LessonShowTools.Core.Models.SettingsWindow;
using LessonShowTools.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace LessonShowTools.Views.SettingPages;

[SettingsPageInfo("test-settings-page", "测试页面", true, SettingsPageCategory.Debug)]
public partial class TestSettingsPage : SettingsPageBase
{
    public static readonly DependencyProperty NavigationUriProperty = DependencyProperty.Register(
        nameof(NavigationUri), typeof(Uri), typeof(TestSettingsPage), new PropertyMetadata(default(Uri)));

    public Uri? NavigationUri
    {
        get { return (Uri)GetValue(NavigationUriProperty); }
        set { SetValue(NavigationUriProperty, value); }
    }

    public TestSettingsPage()
    {
        DataContext = this;
        InitializeComponent();
        Loaded+= OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        var navigationService = NavigationService.GetNavigationService(this);
        navigationService!.Navigated += (sender, args) => NavigationUri =
            (args.ExtraData as SettingsWindowNavigationData)?.NavigateUri;
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService!.Navigate(new Page());
    }
}