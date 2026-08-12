using System;
using LessonShowTools.Core.Abstractions.Automation;
using LessonShowTools.Core.Abstractions.Services;
using LessonShowTools.Core.Attributes;
using MaterialDesignThemes.Wpf;

namespace LessonShowTools.Services.Automation.Triggers;

[TriggerInfo("LessonShowTools.lessons.onClass", "上课时", PackIconKind.BookOutline)]
public class OnClassTrigger(ILessonsService lessonsService) : TriggerBase
{
    private ILessonsService LessonsService { get; } = lessonsService;

    public override void Loaded()
    {
        LessonsService.OnClass += LessonsServiceOnOnClass;
    }
    public override void UnLoaded()
    {
        LessonsService.OnClass -= LessonsServiceOnOnClass;
    }

    private void LessonsServiceOnOnClass(object? sender, EventArgs e)
    {
        Trigger();
    }
}