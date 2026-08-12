using LessonShowTools.Core.Abstractions.Controls;
using LessonShowTools.Core.Abstractions.Services;
using LessonShowTools.Core.Abstractions.Services.Management;
using LessonShowTools.Core.Attributes;
using LessonShowTools.Core.Enums.SettingsWindow;
using LessonShowTools.Services;
using LessonShowTools.ViewModels.SettingsPages;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace LessonShowTools.Views.SettingPages;

/// <summary>
/// GeneralSettingsPage.xaml 的交互逻辑
/// </summary>
[SettingsPageInfo("general", "基本", false , SettingsPageCategory.Internal)]
public partial class GeneralSettingsPage : SettingsPageBase
{
    public SettingsService SettingsService { get; }

    public IManagementService ManagementService { get; }

    public IExactTimeService ExactTimeService { get; }

    public MiniInfoProviderHostService MiniInfoProviderHostService { get; }

    public GeneralSettingsViewModel ViewModel { get; } = new();

    public GeneralSettingsPage(SettingsService settingsService, IManagementService managementService, IExactTimeService exactTimeService, MiniInfoProviderHostService miniInfoProviderHostService)
    {
        InitializeComponent();
        DataContext = this;
        SettingsService = settingsService;
        ManagementService = managementService;
        ExactTimeService = exactTimeService;
        MiniInfoProviderHostService = miniInfoProviderHostService;

        SettingsService.Settings.PropertyChanged+= SettingsOnPropertyChanged;
    }

    private void SettingsOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(SettingsService.Settings.IsTransientDisabled) or nameof(SettingsService.Settings.IsWaitForTransientDisabled))
        {
            RequestRestart();
        }
    }

    private void ButtonSyncTimeNow_OnClick(object sender, RoutedEventArgs e)
    {
        _ = Task.Run(ExactTimeService.Sync);
    }

    private void ButtonCloseMigrationTip_OnClick(object sender, RoutedEventArgs e)
    {
        SettingsService.Settings.ShowComponentsMigrateTip = false;
    }

    private void ButtonWeekOffsetSettingsButtons_OnClick(object sender, RoutedEventArgs e)
    {
        if (e.OriginalSource is not Button)
        {
            return;
        }
        ViewModel.IsWeekOffsetSettingsOpen = false;
    }

    private void ButtonWeekOffsetSettingsOpen_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.IsWeekOffsetSettingsOpen = true;
    }

    private void ButtonCloseSellingAnnouncementBanner_OnClick(object sender, RoutedEventArgs e)
    {
        SettingsService.Settings.ShowSellingAnnouncement = false;
    }

    private void AddTaskAutoStart_OnClick(object sender, RoutedEventArgs e)
    {
        AddTaskRun();
    }

    private void DeleteTaskAutoStart_OnClick(object sender, RoutedEventArgs e)
    {
        DelTaskRun();
    }


    private void AddRunAutoStart_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            WriteRegistryValue("LessonShowTools", System.Windows.Forms.Application.ExecutablePath, @"Software\Microsoft\Windows\CurrentVersion\Run");
        }
        catch (Exception ex)
        {
            App.GetService<ILogger<GeneralSettingsPage>>().LogError(ex, "无法创建开机自启动。");
        }
    }

    private void DeleteRunAutoStart_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            DeleteRegistryValue("LessonShowTools", @"Software\Microsoft\Windows\CurrentVersion\Run");
        }
        catch (Exception ex)
        {
            App.GetService<ILogger<GeneralSettingsPage>>().LogError(ex, "无法删除开机自启动。");
        }
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

    static bool DeleteRegistryValue(string valueName, string subKeyPath)
    {
        try
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(subKeyPath, true))
            {
                if (key == null)
                {
                    return true; // 键不存在，可以认为删除成功
                }

                // 检查值是否存在
                object existingValue = key.GetValue(valueName);
                if (existingValue == null)
                {
                    return true; // 值不存在，可以认为删除成功
                }
                key.DeleteValue(valueName, false);
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
        return false;
    }

    static bool CheckReg(string valueName, string subKeyPath)
    {
        try
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(subKeyPath, false))
            {
                if (key == null)
                {
                    return false;
                }

                object value = key.GetValue(valueName);
                return value != null;
            }
        }
        catch (Exception ex)
        {
            return false;
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


    ///// <summary>
    ///// 判断 LST 任务计划是否存在
    ///// </summary>
    ///// <returns>存在返回 True，否则返回 False</returns>
    //public static bool IsLSTTaskExists()
    //{
    //    string fullTaskName = "\\CJH\\LST";

    //    try
    //    {
    //        ProcessStartInfo psi = new ProcessStartInfo
    //        {
    //            //FileName = "schtasks",
    //            //Arguments = $"/Query /TN \"{fullTaskName}\"",
    //            //UseShellExecute = false,
    //            //CreateNoWindow = true,
    //            //RedirectStandardOutput = true,
    //            //RedirectStandardError = true
    //            FileName = "schtasks",
    //            Arguments = $"/Query /TN \"{fullTaskName}\"",
    //            UseShellExecute = true,  // 设置为 true 以使用 Shell 执行
    //            Verb = "runas",          // 以管理员身份运行
    //            RedirectStandardOutput = false,  // 当 UseShellExecute = true 时，不能重定向输出
    //            RedirectStandardError = false,
    //            CreateNoWindow = true,   // 不创建窗口
    //            WindowStyle = ProcessWindowStyle.Hidden  // 窗口样式为隐藏
    //        };

    //        using (Process process = Process.Start(psi))
    //        {
    //            process.WaitForExit(3000);
    //            return process.ExitCode == 0;  // ExitCode 0 表示任务存在
    //        }
    //    }
    //    catch
    //    {
    //        return false;
    //    }
    //}

    //private static bool IsTaskExistsInRegistry()
    //{
    //    try
    //    {
    //        string registryPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Schedule\TaskCache\Tree\CJH\LST";

    //        using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryPath))
    //        {
    //            return key != null;
    //        }
    //    }
    //    catch
    //    {
    //        return false;
    //    }
    //}

    public void DelTaskRun()
    {
        string taskName = "LST";
        string taskPath = "\\LST";  // 子目录路径
        string fullTaskName = taskPath + "\\" + taskName;  // 完整任务名称: \LST\LST

        try
        {
            ExecuteSchtasks($"/Delete /TN \"{fullTaskName}\" /F");
        }
        catch (Exception ex)
        {
            // Console.WriteLine($"错误: {ex.Message}");
            App.GetService<ILogger<GeneralSettingsPage>>().LogError(ex, "无法创建开机自启动。");
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
        string tempXmlPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"LSTTask_{DateTime.Now:yyyyMMddHHmmss}.xml");

        try
        {
            // 1. 从内嵌资源读取 XML 内容
            string xmlContent = ReadEmbeddedResource("Assets.LSTTask.xml");

            if (string.IsNullOrEmpty(xmlContent))
            {
                App.GetService<ILogger<GeneralSettingsPage >>().LogDebug("错误: 无法从内嵌资源读取 XML 模板文件");
                return;
            }

            App.GetService<ILogger<GeneralSettingsPage >>().LogDebug("成功从内嵌资源读取 XML 模板");

            // 2. 替换程序路径
            string oldExeName = "LessonShowTools.exe";

            if (xmlContent.Contains(oldExeName))
            {
                xmlContent = xmlContent.Replace(oldExeName, currentExePath);
                App.GetService<ILogger<GeneralSettingsPage >>().LogDebug($"已将 {oldExeName} 替换为: {currentExePath}");
            }
            else
            {
                App.GetService<ILogger<GeneralSettingsPage >>().LogDebug($"警告: XML 中未找到 {oldExeName}，跳过替换");
            }

            // 3. 写入临时目录（使用 UTF-16 编码，任务计划要求）
            File.WriteAllText(tempXmlPath, xmlContent, Encoding.Unicode);
            App.GetService<ILogger<GeneralSettingsPage >>().LogDebug($"已创建临时 XML: {tempXmlPath}");
            // 4. 删除现有任务（如果存在）
            ExecuteSchtasks($"/Delete /TN \"{fullTaskName}\" /F");

            // 5. 使用 XML 文件创建任务
            string arguments = $"/Create /TN \"{fullTaskName}\" /XML \"{tempXmlPath}\" /F";
            string result = ExecuteSchtasks(arguments);

        }
        catch (Exception ex)
        {
            App.GetService<ILogger<GeneralSettingsPage >>().LogDebug($"错误: {ex.Message}");
            App.GetService<ILogger<GeneralSettingsPage >>().LogDebug($"详细信息: {ex.StackTrace}");
        }
        finally
        {
            // 6. 清理临时文件
            if (File.Exists(tempXmlPath))
            {
                try
                {
                    File.Delete(tempXmlPath);
                    App.GetService<ILogger<GeneralSettingsPage >>().LogDebug($"已清理临时文件: {tempXmlPath}");
                }
                catch (Exception ex)
                {
                    App.GetService<ILogger<GeneralSettingsPage >>().LogDebug($"清理临时文件失败: {ex.Message}");
                }
            }
        }
    }

 
}