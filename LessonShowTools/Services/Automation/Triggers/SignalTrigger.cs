using LessonShowTools.Core.Abstractions.Automation;
using LessonShowTools.Core.Attributes;
using LessonShowTools.Models.Automation.Triggers;
using LessonShowTools.Models.EventArgs;
using MaterialDesignThemes.Wpf;

namespace LessonShowTools.Services.Automation.Triggers;

[TriggerInfo("LessonShowTools.signal", "收到信号时", PackIconKind.AlertOctagramOutline)]
public class SignalTrigger(SignalTriggerHandlerService signalTriggerHandlerService) : TriggerBase<SignalTriggerSettings>
{
    public SignalTriggerHandlerService SignalTriggerHandlerService { get; } = signalTriggerHandlerService;

    public override void Loaded()
    {
        SignalTriggerHandlerService.Handled += SignalTriggerHandlerServiceOnHandled;
    }


    public override void UnLoaded()
    {
        SignalTriggerHandlerService.Handled -= SignalTriggerHandlerServiceOnHandled;
    }

    private void SignalTriggerHandlerServiceOnHandled(object? sender, SignalTriggerEventArgs e)
    {
        if (e.SignalName != Settings.SignalName) return;

        if (e.Revert)
        {
            TriggerRevert();
        }
        else
        {
            Trigger();
        }
    }
}