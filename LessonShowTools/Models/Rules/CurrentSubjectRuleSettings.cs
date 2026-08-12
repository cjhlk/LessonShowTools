using CommunityToolkit.Mvvm.ComponentModel;

namespace LessonShowTools.Models.Rules;

public class CurrentSubjectRuleSettings : ObservableRecipient
{
    private string _subjectId = "";

    public string SubjectId
    {
        get => _subjectId;
        set
        {
            if (value == _subjectId) return;
            _subjectId = value;
            OnPropertyChanged();
        }
    }
}