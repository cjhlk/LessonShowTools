using System.Collections.ObjectModel;
using LessonShowTools.Core.Abstractions.Models;
using LessonShowTools.Core.Models.Ruleset;
using CommunityToolkit.Mvvm.ComponentModel;

namespace LessonShowTools.Models.ComponentSettings;

public partial class SlideComponentSettings : ObservableObject, IComponentContainerSettings
{
    [ObservableProperty] private ObservableCollection<Core.Models.Components.ComponentSettings> _children = new();

    [ObservableProperty] private double _slideSeconds = 15;

    /// <summary>
    /// 轮换模式
    /// </summary>
    /// <value>
    /// 0 - 循环 <br/>
    /// 1 - 随机 <br/>
    /// 2 - 往复
    /// </value>
    [ObservableProperty] private int _slideMode = 0;

    [ObservableProperty] private bool _isPauseOnRuleEnabled = false;

    [ObservableProperty] private Ruleset _pauseRule = new();

    [ObservableProperty] private bool _isStopOnRuleEnabled = false;

    [ObservableProperty] private Ruleset _stopRule = new();
}