using System.Windows.Controls;
using LessonShowTools.Core.Abstractions.Controls;
using LessonShowTools.Core.Attributes;

namespace LessonShowTools.ExamplePlugin.Views.SettingsPages;

[SettingsPageInfo("LessonShowTools.example-plugin.hello", "Hello world!")]
public partial class HelloSettingsPage : SettingsPageBase
{
    public HelloSettingsPage()
    {
        InitializeComponent();
    }
}