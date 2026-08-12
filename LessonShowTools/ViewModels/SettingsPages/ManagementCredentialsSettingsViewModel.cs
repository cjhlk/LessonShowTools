using CommunityToolkit.Mvvm.ComponentModel;

namespace LessonShowTools.ViewModels.SettingsPages;

public partial class ManagementCredentialsSettingsViewModel : ObservableObject
{
    [ObservableProperty] private bool _isLocked = true;
}