using System.Collections.ObjectModel;
using LessonShowTools.Core.Abstractions.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace LessonShowTools.Models.ComponentSettings;

public partial class GroupComponentSettings : ObservableObject, IComponentContainerSettings
{
    [ObservableProperty]
    private ObservableCollection<Core.Models.Components.ComponentSettings> _children = [];
}