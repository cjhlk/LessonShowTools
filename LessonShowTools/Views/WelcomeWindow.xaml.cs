using LessonShowTools.Controls;
using LessonShowTools.Core;
using LessonShowTools.Core.Abstractions.Services.Management;
using LessonShowTools.Core.Controls;
using LessonShowTools.Helpers;
using LessonShowTools.Services;
using LessonShowTools.Services.Management;
using LessonShowTools.ViewModels;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using WindowsShortcutFactory;
using Path = System.IO.Path;

namespace LessonShowTools.Views;
/// <summary>
/// WelcomeWindow.xaml 的交互逻辑
/// </summary>
public partial class WelcomeWindow : MyWindow
{
    public WelcomeViewModel ViewModel
    {
        get;
        set;
    } = new();

    public SettingsService SettingsService { get; } = App.GetService<SettingsService>();

    public IManagementService ManagementService { get; } = App.GetService<IManagementService>();

    public WelcomeWindow()
    {
        DataContext = this;
        InitializeComponent();
        var reader = new StreamReader(Application.GetResourceStream(new Uri("/Assets/License.txt", UriKind.Relative))!
            .Stream);
        ViewModel.License = reader.ReadToEnd();
        ViewModel.Settings = SettingsService.Settings;
    }

    protected override async void OnContentRendered(EventArgs e)
    {
        base.OnContentRendered(e);
        ViewModel.MasterTabIndex = 1;
    }

    static bool WriteRegistryValue(string valueName, string valueData, string subKeyPath)
    {
        try
        {
            // 打开或创建子键
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(subKeyPath, true))
            {
                if (key == null)
                {
                    // 如果键不存在，创建它
                    using (RegistryKey createdKey = Registry.CurrentUser.CreateSubKey(subKeyPath))
                    {
                        createdKey?.SetValue(valueName, valueData, RegistryValueKind.String);
                        return true;
                    }
                }
                else
                {
                    // 键已存在，直接写入
                    key.SetValue(valueName, valueData, RegistryValueKind.String);
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    /// <summary>
    /// 从内嵌资源读取文件内容
    /// </summary>
    /// <param name="resourceName">资源文件名</param>
    /// <returns>文件内容字符串</returns>
    /// <summary>
    /// 从内嵌资源读取文件内容
    /// </summary>
    /// <param name="resourcePath">资源路径</param>
    /// <returns>文件内容字符串</returns>
    private static string ReadEmbeddedResource(string resourcePath)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        string fullResourceName = $"{assembly.GetName().Name}.{resourcePath}";

        using (Stream stream = assembly.GetManifestResourceStream(fullResourceName))
        {
            if (stream == null)
            {
                return null;
            }

            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }

    public void AddTaskRun()
    {
        string taskName = "LST";
        string taskPath = "\\LST";  // 子目录路径
        string fullTaskName = taskPath + "\\" + taskName;  // 完整任务名称: \LST\LST
        string currentExePath = Process.GetCurrentProcess().MainModule.FileName;
        // 临时 XML 文件路径
        string tempXmlPath = Path.Combine(Path.GetTempPath(), $"LSTTask_{DateTime.Now:yyyyMMddHHmmss}.xml");

        try
        {
            // 1. 从内嵌资源读取 XML 内容
            string xmlContent = ReadEmbeddedResource("Assets.LSTTask.xml");

            if (string.IsNullOrEmpty(xmlContent))
            {
                 App.GetService<ILogger<WelcomeWindow>>().LogDebug("错误: 无法从内嵌资源读取 XML 模板文件");
                return;
            }

             App.GetService<ILogger<WelcomeWindow>>().LogDebug("成功从内嵌资源读取 XML 模板");

            // 2. 替换程序路径
            string oldExeName = "LessonShowTools.exe";

            if (xmlContent.Contains(oldExeName))
            {
                xmlContent = xmlContent.Replace(oldExeName, currentExePath);
                 App.GetService<ILogger<WelcomeWindow>>().LogDebug($"已将 {oldExeName} 替换为: {currentExePath}");
            }
            else
            {
                 App.GetService<ILogger<WelcomeWindow>>().LogDebug($"警告: XML 中未找到 {oldExeName}，跳过替换");
            }

            // 3. 写入临时目录（使用 UTF-16 编码，任务计划要求）
            File.WriteAllText(tempXmlPath, xmlContent, Encoding.Unicode);
             App.GetService<ILogger<WelcomeWindow>>().LogDebug($"已创建临时 XML: {tempXmlPath}");
            // 4. 删除现有任务（如果存在）
            ExecuteSchtasks($"/Delete /TN \"{fullTaskName}\" /F");

            // 5. 使用 XML 文件创建任务
            string arguments = $"/Create /TN \"{fullTaskName}\" /XML \"{tempXmlPath}\" /F";
            string result = ExecuteSchtasks(arguments);

        }
        catch (Exception ex)
        {
             App.GetService<ILogger<WelcomeWindow>>().LogDebug($"错误: {ex.Message}");
             App.GetService<ILogger<WelcomeWindow>>().LogDebug($"详细信息: {ex.StackTrace}");
        }
        finally
        {
            // 6. 清理临时文件
            if (File.Exists(tempXmlPath))
            {
                try
                {
                    File.Delete(tempXmlPath);
                     App.GetService<ILogger<WelcomeWindow>>().LogDebug($"已清理临时文件: {tempXmlPath}");
                }
                catch (Exception ex)
                {
                     App.GetService<ILogger<WelcomeWindow>>().LogDebug($"清理临时文件失败: {ex.Message}");
                }
            }
        }
    }

    private static string ExecuteSchtasks(string arguments)
    {
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "schtasks",
            Arguments = arguments,
            UseShellExecute = true,  // 设置为 true 以使用 Shell 执行
            Verb = "runas",          // 以管理员身份运行
            RedirectStandardOutput = false,  // 当 UseShellExecute = true 时，不能重定向输出
            RedirectStandardError = false,
            CreateNoWindow = true,   // 不创建窗口
            WindowStyle = ProcessWindowStyle.Hidden  // 窗口样式为隐藏
        };

        try
        {
            using (Process process = Process.Start(psi))
            {
                process.WaitForExit();

                //// 由于无法重定向输出，返回简单的状态信息
                if (process.ExitCode == 0)
                {
                    return "SUCCESS";
                }
                else
                {
                    return $"命令执行失败，退出代码: {process.ExitCode}";
                }
            }
        }
        catch (Exception ex)
        {
            return $"执行失败: {ex.Message}";
        }
    }



    private async void ButtonClose_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.IsExitConfirmed = true;
        DialogResult = true;
        var desktopPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "LessonShowTools 课程表小工具.lnk");
        using var shortcut = new WindowsShortcut();
        shortcut.Path = Environment.ProcessPath;
        shortcut.WorkingDirectory = Environment.CurrentDirectory;
        try
        {
            if (ViewModel.RegisterUrlScheme)
                UriProtocolRegisterHelper.Register();
            if (ViewModel.CreateStartupShortcut)
                //shortcut.Save(startupPath);
                //WriteRegistryValue("LessonShowTools", System.Windows.Forms.Application.ExecutablePath, @"Software\Microsoft\Windows\CurrentVersion\Run");
                AddTaskRun();
            //if (ViewModel.CreateStartMenuShortcut)
            //    shortcut.Save(startMenuPath);
            if (ViewModel.CreateDesktopShortcut)
                shortcut.Save(desktopPath);
            if (ViewModel is { CreateClassSwapShortcut: true, RegisterUrlScheme: true })
                await ShortcutHelpers.CreateClassSwapShortcutAsync();
        }
        catch (Exception ex)
        {
            App.GetService<ILogger<WelcomeWindow>>().LogError(ex, "无法创建快捷方式。");
        }

        Close();
        if (ViewModel.RequiresRestarting)
        {
            AppBase.Current.Restart();
        }
    }

    private async void WelcomeWindow_OnClosing(object? sender, CancelEventArgs e)
    {
        if (ViewModel.IsExitConfirmed)
        {
            return;
        }

        e.Cancel = true;
        if (DialogHost.IsDialogOpen(ViewModel.DialogId))
        {
            return;
        }
        var r = await DialogHost.Show(FindResource("ExitAppConfirmDialog"), ViewModel.DialogId);
        if ((bool?)r == true)
        {
            ViewModel.IsExitConfirmed = true;
            Close();
        }
    }

    private void ButtonFlipNext_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.FlipNextCount++;
        ViewModel.FlipIndex = ViewModel.FlipIndex + 1 >= 3 ? 0 : ViewModel.FlipIndex + 1;
        if (ViewModel.FlipIndex >= 2)
            ViewModel.IsFlipEnd = true;
    }

    private async void ButtonJoinManagementOnClick(object sender, RoutedEventArgs e)
    {
        await DialogHost.Show(new JoinManagementDialog(), ViewModel.DialogId);
    }

    private async void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
    {
    }

    private void ButtonSkip_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.SlideIndex = 4;
        ViewModel.SnackbarQueue.Enqueue("您稍后可以在【应用设置】中调整这些设置。");
    }

    private void ButtonCompleteFlipBack_OnClick(object sender, RoutedEventArgs e)
    {
        if (ViewModel.FlipIndex > 0)
        {
            ViewModel.FlipIndex--;
            ViewModel.IsFlipEnd = ViewModel.FlipIndex >= 2;
        }
        else
        {
            ViewModel.SlideIndex--;
        }
    }
}