using CommunityToolkit.Mvvm.ComponentModel;

namespace LessonShowTools.ViewModels.SettingsPages;

public partial class ManagementPolicySettingsViewModel : ObservableObject
{
    [ObservableProperty] private bool _isLocked = true;
}