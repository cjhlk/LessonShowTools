using CommunityToolkit.Mvvm.ComponentModel;

namespace LessonShowTools.ViewModels.SettingsPages;

public class GeneralSettingsViewModel : ObservableRecipient
{
    private bool _isWeekOffsetSettingsOpen = false;

    public bool IsWeekOffsetSettingsOpen
    {
        get => _isWeekOffsetSettingsOpen;
        set
        {
            if (value == _isWeekOffsetSettingsOpen) return;
            _isWeekOffsetSettingsOpen = value;
            OnPropertyChanged();
        }
    }
}