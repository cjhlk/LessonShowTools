using System;
using LessonShowTools.Core.Abstractions.Automation;
using LessonShowTools.Core.Abstractions.Services;
using LessonShowTools.Core.Attributes;
using MaterialDesignThemes.Wpf;

namespace LessonShowTools.Services.Automation.Triggers;

[TriggerInfo("LessonShowTools.lessons.onAfterSchool", "放学时", PackIconKind.ExitRun)]
public class OnAfterSchoolTrigger(ILessonsService lessonsService) : TriggerBase
{
    private ILessonsService LessonsService { get; } = lessonsService;

    public override void Loaded()
    {
        LessonsService.OnAfterSchool += OnLessonsServiceOnAfterSchool;
    }
    public override void UnLoaded()
    {
        LessonsService.OnAfterSchool -= OnLessonsServiceOnAfterSchool;
    }

    private void OnLessonsServiceOnAfterSchool(object? sender, EventArgs e)
    {
        Trigger();
    }
}