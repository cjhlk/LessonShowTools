using System;
using LessonShowTools.Core.Abstractions.Services;
using LessonShowTools.Shared;
namespace LessonShowTools;

public static class DateTimeToCurrentDateTimeConverter
{
    public static DateTime Convert(DateTime dateTime)
    {
        var now = IAppHost.GetService<IExactTimeService>().GetCurrentLocalDateTime();
        return new DateTime(now.Year, now.Month, now.Day, dateTime.Hour, dateTime.Minute,
            dateTime.Second);
    }
}