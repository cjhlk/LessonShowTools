using System.Windows.Media.Animation;

namespace LessonShowTools.Models.EventArgs;

public class MainWindowAnimationEventArgs(string? storyboardName) : System.EventArgs
{
    public string? StoryboardName { get; } = storyboardName;
}