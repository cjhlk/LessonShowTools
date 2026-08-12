using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using LessonShowTools.Core.Attributes;
using LessonShowTools.Core.Models.Components;

namespace LessonShowTools.Core.Services.Registry;

/// <summary>
/// 组件注册服务
/// </summary>
public class ComponentRegistryService
{
    /// <summary>
    /// 已注册的组件
    /// </summary>
    public static ObservableCollection<ComponentInfo> Registered { get; } = new();

    public static ObservableCollection<ComponentSettings> RegisteredSettings { get; } = new();

    public static Dictionary<Guid, Guid> MigrationPairs { get; } = new();
}