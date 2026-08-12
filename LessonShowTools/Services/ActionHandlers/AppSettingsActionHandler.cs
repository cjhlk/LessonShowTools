using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using LessonShowTools.Core;
using LessonShowTools.Core.Abstractions.Services;
using LessonShowTools.Models.Actions;
using Microsoft.Extensions.Hosting;
namespace LessonShowTools.Services.ActionHandlers;

public class AppSettingsActionHandler : IHostedService
{
    public AppSettingsActionHandler(SettingsService SettingsService, IActionService ActionService)
    {
        var Reg = ActionService.RegisterActionHandler;
        var RegRevert = ActionService.RegisterRevertHandler;

        Reg("LessonShowTools.settings.currentComponentConfig", (s, g) =>
            Add(g, "CurrentComponentConfig", ((CurrentComponentConfigActionSettings)s!).Value));
        Reg("LessonShowTools.settings.theme", (s, g) => 
            Add(g, "Theme", ((ThemeActionSettings)s!).Value));
        Reg("LessonShowTools.settings.windowDockingLocation", (s, g) =>
            Add(g, "WindowDockingLocation", ((WindowDockingLocationActionSettings)s!).Value));

        RegRevert("LessonShowTools.settings.currentComponentConfig", (s, g) => 
            Remove(g, "CurrentComponentConfig"));
        RegRevert("LessonShowTools.settings.theme", (s, g) => 
            Remove(g, "Theme"));
        RegRevert("LessonShowTools.settings.windowDockingLocation", (s, g) => 
            Remove(g, "WindowDockingLocation"));

        void Add(string g, string binding, dynamic value)
        {
            App.Current.Dispatcher.Invoke(
                new Action(() => SettingsService.AddSettingsOverlay(g, binding, value)),
                DispatcherPriority.Render);
        }

        void Remove(string g, string binding)
        {
            App.Current.Dispatcher.Invoke(
                new Action(() => SettingsService.RemoveSettingsOverlay(g, binding)),
                DispatcherPriority.Render);
        }
    }

    public async Task StartAsync(CancellationToken _) { }
    public async Task StopAsync(CancellationToken _) { }
}
