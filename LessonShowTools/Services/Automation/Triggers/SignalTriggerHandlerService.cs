using System;
using LessonShowTools.Core.Abstractions.Services;
using LessonShowTools.Models.Automation.Triggers;
using LessonShowTools.Models.EventArgs;

namespace LessonShowTools.Services.Automation.Triggers;

public class SignalTriggerHandlerService
{
    public event EventHandler<SignalTriggerEventArgs>? Handled;

    public void EmitSignal(string name, bool revert)
    {
        Handled?.Invoke(this, new SignalTriggerEventArgs(name, revert));
    }

    public SignalTriggerHandlerService(IActionService actionService)
    {
        actionService.RegisterActionHandler("LessonShowTools.broadcastSignal", (o, guid) =>
        {
            if (o is SignalTriggerSettings settings)
            {
                EmitSignal(settings.SignalName, settings.IsRevert);
            }
        });
        actionService.RegisterRevertHandler("LessonShowTools.broadcastSignal", (o, guid) =>
        {
            if (o is SignalTriggerSettings settings)
            {
                EmitSignal(settings.SignalName, !settings.IsRevert);
            }
        });
    }
}