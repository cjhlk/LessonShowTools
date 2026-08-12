using CommunityToolkit.Mvvm.ComponentModel;

namespace LessonShowTools.Shared.Models.Profile;

/// <summary>
/// 代表一个预定的课表。
/// </summary>
public class OrderedSchedule : ObservableRecipient
{
    private string _classPlanId = "";

    /// <summary>
    /// 预定课表 ID
    /// </summary>
    public string ClassPlanId
    {
        get => _classPlanId;
        set
        {
            if (value == _classPlanId) return;
            _classPlanId = value;
            OnPropertyChanged();
        }
    }
}