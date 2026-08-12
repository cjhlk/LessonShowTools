using LessonShowTools.Core.Abstractions;
using LessonShowTools.Core.Attributes;
using LessonShowTools.Core.Extensions.Registry;
using LessonShowTools.ExamplePlugin.Views.SettingsPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LessonShowTools.ExamplePlugin;

[PluginEntrance]
public class Plugin : PluginBase
{
    public override void Initialize(HostBuilderContext context, IServiceCollection services)
    {
        Console.WriteLine("Hello world!");
        services.AddSettingsPage<HelloSettingsPage>();
    }
}