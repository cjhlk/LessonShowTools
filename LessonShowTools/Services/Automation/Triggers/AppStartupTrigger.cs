using System;
using System.Diagnostics;
using System.Linq;
using LessonShowTools.Core;
using LessonShowTools.Core.Abstractions.Automation;
using LessonShowTools.Core.Attributes;
using MaterialDesignThemes.Wpf;

namespace LessonShowTools.Services.Automation.Triggers;

[TriggerInfo("LessonShowTools.lifetime.startup", "应用启动时", PackIconKind.AutoStart)]
public class AppStartupTrigger : TriggerBase
{
    public override void Loaded()
    {
        var stack = new StackTrace();
        if (stack.GetFrames().FirstOrDefault(x => x.GetMethod()?.DeclaringType == typeof(App)) != null)
        {
            Trigger();
        }
    }

    public override void UnLoaded()
    {
    }
}