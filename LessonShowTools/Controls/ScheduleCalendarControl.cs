using System;
using System.Windows;
using System.Windows.Controls;

namespace LessonShowTools.Controls;

public class ScheduleCalendarControl : Calendar
{
    public event EventHandler? ScheduleUpdated;

    public void UpdateSchedule()
    {
        ScheduleUpdated?.Invoke(this, EventArgs.Empty);
    }
}