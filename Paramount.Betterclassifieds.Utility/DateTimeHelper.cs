using System;

namespace Paramount
{
    public static class DateTimeHelper
    {
        public static DateTime DateForNext(DayOfWeek dayOfWeek)
        {
            var daysUntilNextRequiredDayOfWeek = ((int)dayOfWeek - (int)DateTime.Today.DayOfWeek + 7) % 7;
            var nextDate = DateTime.Today.AddDays(daysUntilNextRequiredDayOfWeek);
            return nextDate;
        }
    }
}