using LessonShowTools.Core.Abstractions.Automation;
using LessonShowTools.Core.Attributes;
using LessonShowTools.Models.Automation.Triggers;
using LessonShowTools.Models.EventArgs;
using MaterialDesignThemes.Wpf;

namespace LessonShowTools.Services.Automation.Triggers;

[TriggerInfo("LessonShowTools.uri", "调用 Uri 时", PackIconKind.Link)]
public class UriTrigger(UriTriggerHandlerService uriTriggerHandlerService) : TriggerBase<UriTriggerSettings>
{
    private UriTriggerHandlerService UriTriggerHandlerService { get; } = uriTriggerHandlerService;

    public override void Loaded()
    {
        UriTriggerHandlerService.HandledRun += UriTriggerHandlerServiceOnHandledRun;
        UriTriggerHandlerService.HandledRevert += UriTriggerHandlerServiceOnHandledRevert;
    }

    private void UriTriggerHandlerServiceOnHandledRevert(object? sender, UriTriggerHandledEventArgs e)
    {
        if (e.Name == Settings.UriSuffix)
        {
            TriggerRevert();
        }
    }

    private void UriTriggerHandlerServiceOnHandledRun(object? sender, UriTriggerHandledEventArgs e)
    {
        if (e.Name == Settings.UriSuffix)
        {
            Trigger();
        }
    }

    public override void UnLoaded()
    {
        UriTriggerHandlerService.HandledRun -= UriTriggerHandlerServiceOnHandledRun;
        UriTriggerHandlerService.HandledRevert -= UriTriggerHandlerServiceOnHandledRevert;
    }
}