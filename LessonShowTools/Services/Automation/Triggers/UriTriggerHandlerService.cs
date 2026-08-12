using LessonShowTools.Core.Abstractions.Services;
using LessonShowTools.Models.EventArgs;
using System;

namespace LessonShowTools.Services.Automation.Triggers;

public class UriTriggerHandlerService
{
    private IUriNavigationService UriNavigationService { get; }

    internal event EventHandler<UriTriggerHandledEventArgs>? HandledRun;
    internal event EventHandler<UriTriggerHandledEventArgs>? HandledRevert;

    public UriTriggerHandlerService(IUriNavigationService uriNavigationService)
    {
        UriNavigationService = uriNavigationService;

        UriNavigationService.HandleAppNavigation("api/automation/run", args =>
        {
            HandledRun?.Invoke(this, new UriTriggerHandledEventArgs(string.Join('/', args.ChildrenPathPatterns)));
        });
        UriNavigationService.HandleAppNavigation("api/automation/revert", args =>
        {
            HandledRevert?.Invoke(this, new UriTriggerHandledEventArgs(string.Join('/', args.ChildrenPathPatterns)));
        });
    }
}