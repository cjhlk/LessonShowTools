namespace LessonShowTools.Shared.IPC;

/// <summary>
/// 存储了 LessonShowTools 内置的跨进程通信路由通知标识符。
/// </summary>
public static class IpcRoutedNotifyIds
{
    /// <summary>
    /// 上课事件通知标识符
    /// </summary>
    public const string OnClassNotifyId = "LessonShowTools.lessonsService.onClass";

    /// <summary>
    /// 课间休息事件通知标识符
    /// </summary>
    public const string OnBreakingTimeNotifyId = "LessonShowTools.lessonsService.onBreakingTime";

    /// <summary>
    /// 放学事件通知标识符
    /// </summary>
    public const string OnAfterSchoolNotifyId = "LessonShowTools.lessonsService.onAfterSchool";

    /// <summary>
    /// 当前时间点状态通知标识符
    /// </summary>
    public const string CurrentTimeStateChangedNotifyId = "LessonShowTools.lessonsService.currentTimeStateChanged";
}