using System;
using System.Diagnostics;
using System.Web;
using System.Windows;

using LessonShowTools.Controls;
using LessonShowTools.Core;
using LessonShowTools.Core.Controls;

namespace LessonShowTools.Views;

/// <summary>
/// CrashWindow.xaml 的交互逻辑
/// </summary>
public partial class CrashWindow : MyWindow
{
    public string? CrashInfo
    {
        get;
        set;
    } = "";

    public bool IsCritical { get; set; } = false;

    public bool AllowIgnore { get; set; } = true;

    public CrashWindow()
    {
        InitializeComponent();
        DataContext = this;
    }

    private void ButtonIgnore_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void ButtonExit_OnClick(object sender, RoutedEventArgs e)
    {
        if (IsCritical)
        {
            Environment.Exit(1);
        }
        else
        {
            Application.Current.Shutdown();
        }
    }

    private void ButtonRestart_OnClick(object sender, RoutedEventArgs e)
    {
        AppBase.Current.Restart();
    }

    private void ButtonCopy_OnClick(object sender, RoutedEventArgs e)
    {
        TextBoxCrashInfo.Focus();
        TextBoxCrashInfo.SelectAll();
        TextBoxCrashInfo.Copy();
    }

   

    private void ButtonDebug_OnClick(object sender, RoutedEventArgs e)
    {
        if (Debugger.Launch())
        {
            Close();
        }
    }
}