using System.Reflection;
using System.Windows;
using LessonShowTools.Shared;

namespace LessonShowTools.Core;

/// <summary>
/// 应用对象基类
/// </summary>
public abstract class AppBase : Application, IAppHost
{
    /// <summary>
    /// 获取当前应用程序实例。
    /// </summary>
    public new static AppBase Current => (Application.Current as AppBase)!;

    /// <summary>
    /// 重启应用程序。
    /// </summary>
    /// <param name="quiet">是否静默重启</param>
    public abstract void Restart(bool quiet=false);

    /// <summary>
    /// 停止当前应用程序。
    /// </summary>
    public abstract void Stop();

    /// <summary>
    /// 获取应用是否已裁剪资源。
    /// </summary>
    /// <returns></returns>
    public abstract bool IsAssetsTrimmed();

    /// <summary>
    /// 应用是否属于开发构建
    /// </summary>
    public abstract bool IsDevelopmentBuild { get; }

    /// <summary>
    /// 应用是否处于 MSIX 打包
    /// </summary>
    public abstract bool IsMsix { get; }

    /// <summary>
    /// 当应用启动时触发。
    /// </summary>
    public abstract event EventHandler? AppStarted;

    /// <summary>
    /// 当应用正在停止时触发。
    /// </summary>
    public abstract event EventHandler? AppStopping;

    /// <summary>
    /// 应用打包类型
    /// </summary>
    public string PackagingType => IsMsix ? "MSIX" : "Win32";

    /// <summary>
    /// 应用分发频道
    /// </summary>
    public string AppSubChannel => $"Windows-x64-{AppCodeName}-{(IsAssetsTrimmed() ? "Trimmed" : "Full")}-{PackagingType}";

    internal AppBase()
    {
    }

    /// <summary>
    /// 应用版本
    /// </summary>
    public static string AppVersion => Assembly.GetExecutingAssembly().GetName().Version!.ToString();

    /// <summary>
    /// 应用版本代号
    /// </summary>
    // ReSharper disable once StringLiteralTypo
    public static string AppCodeName => "GGMSV";

    /// <summary>
    /// 应用长版本号
    /// </summary>
    public static string AppVersionLong =>
        $"{AppVersion} {AppCodeName}";
    //  $"{AppVersion}-{AppCodeName}-Core1.6.0.1";
}