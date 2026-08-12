using System.ComponentModel;

namespace LessonShowTools.Shared.Interfaces;

public interface ITimeRule : INotifyPropertyChanged
{
    public string Name
    {
        get;
        set;
    }

    public bool IsSatisfied
    {
        get; set;
    }
}