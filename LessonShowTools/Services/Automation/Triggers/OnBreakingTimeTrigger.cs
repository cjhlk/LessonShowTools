using System;
using LessonShowTools.Core.Abstractions.Automation;
using LessonShowTools.Core.Abstractions.Services;
using LessonShowTools.Core.Attributes;
using MaterialDesignThemes.Wpf;

namespace LessonShowTools.Services.Automation.Triggers;

[TriggerInfo("LessonShowTools.lessons.onBreakingTime", "课间休息时", PackIconKind.ClockOutline)]
public class OnBreakingTimeTrigger(ILessonsService lessonsService) : TriggerBase
{
    private ILessonsService LessonsService { get; } = lessonsService;

    public override void Loaded()
    {
        LessonsService.OnBreakingTime += LessonsServiceOnOnBreakingTime;
    }
    public override void UnLoaded()
    {
        LessonsService.OnBreakingTime -= LessonsServiceOnOnBreakingTime;
    }

    private void LessonsServiceOnOnBreakingTime(object? sender, EventArgs e)
    {
        Trigger();
    }
}