using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace LessonShowTools.Models.Authorize;

public partial class Credential : ObservableObject
{
    [ObservableProperty] private ObservableCollection<CredentialItem> _items = [];
}