namespace Vipl.Base.Extensions;

/// <summary> Extensions for <see cref="DateTime"/>. </summary>
public static class DateExtension
{
    /// <summary> Split period between <paramref name="start"/> and <paramref name="end"/> to monthly periods </summary>
    /// <param name="start">Start of first period.</param>
    /// <param name="end">End of last period.</param>
    /// <param name="cal">Calendar used.</param>
    /// <returns><see cref="IEnumerable{T}"/> of <see cref="DateTimeRange"/> for each month  between <paramref name="start"/> and <paramref name="end"/>.</returns>
    public static IEnumerable<DateTimeRange> SplitByMonths(this DateTime start, DateTime end, System.Globalization.Calendar? cal = default)
    {
        cal ??= System.Globalization.CultureInfo.CurrentCulture.Calendar;
        return from y in Enumerable.Range(start.Year, end.Year - start.Year + 1)
            let maxMonth = y < end.Year ? cal.GetMonthsInYear(y) : end.Month
            let minMonth = y > start.Year ? 1 : start.Month
            from m in Enumerable.Range(minMonth, maxMonth - minMonth + 1)
            let isStart = y == start.Year && m == start.Month
            let isEnd = y == end.Year && m == end.Month
            let startDate = isStart ? start : new DateTime(y, m, 1)
            let endDate = isEnd ? end : new DateTime(y, m, cal.GetDaysInMonth(y, m))
            select new DateTimeRange
            {
                StartDate = startDate,
                EndDate = endDate
            };
    }
    /// <summary> Calculate Percent of month is in this period. 1 is 100%. </summary>
    /// <param name="range">Period for calculation.</param>
    /// <param name="cal">Calendar used.</param>
    /// <returns>If <see cref="DateTimeRange.StartDate"/> and <see cref="DateTimeRange.EndDate"/> is in same month then returned percent is based of number of days in that month.
    /// If they are in different months then percent is based on 30 days month.</returns>
    public static double PercentOfTheMonth(this DateTimeRange range, System.Globalization.Calendar? cal = default)
    {
        cal ??= System.Globalization.CultureInfo.CurrentCulture.Calendar;
        if (new DateTime(range.StartDate.Year, range.StartDate.Month, 1) != new DateTime(range.EndDate.Year, range.EndDate.Month, 1))
        {
            return (range.EndDate - range.StartDate).TotalDays / 30;
        }
        return (range.EndDate.Day - range.StartDate.Day + 1d) / cal.GetDaysInMonth(range.StartDate.Year, range.StartDate.Month);
    }
    /// <summary> Calculate each day in given period between <paramref name="fromDateTime"/> and <paramref name="toDateTime"/> </summary>
    /// <param name="fromDateTime">Start date.</param>
    /// <param name="toDateTime">End date.</param>
    /// <returns><see cref="IEnumerable{T}"/> of <see cref="DateTime"/> for each day in given period.</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static IEnumerable<DateTime> EachDay(this DateTime fromDateTime, DateTime toDateTime)
    {
        for (var currentDateTime = fromDateTime.Date; currentDateTime <= toDateTime.Date; currentDateTime = currentDateTime.AddDays(1.0))
            yield return currentDateTime;
    }
    /// <summary> Calculate each day in given period in given period </summary>
    /// <param name="range">Start period.</param>
    /// <returns><see cref="IEnumerable{T}"/> of <see cref="DateTime"/> for each day in given period.</returns>
    public static IEnumerable<DateTime> EachDay(this DateTimeRange range)
    {
        return range.StartDate.EachDay(range.EndDate);
    }
    /// <summary> Generate <see cref="IEnumerable{T}"/> of <see cref="DateTime"/> between <paramref name="startTime"/> and <paramref name="endTime"/> in increments of <paramref name="stepInterval"/> </summary>
    /// <param name="stepInterval">Incremental step.</param>
    /// <param name="startTime">Start time for generation</param>
    /// <param name="endTime">End time for generation.</param>
    /// <returns><see cref="IEnumerable{T}"/> of <see cref="DateTime"/> between <paramref name="startTime"/> and <paramref name="endTime"/> in increments of <paramref name="stepInterval"/></returns>
    public static IEnumerable<DateTime> GenerateTimeNodes(this DateInterval stepInterval, DateTime startTime, DateTime endTime)
    {
        for (var intervalTime = startTime; intervalTime < endTime;)
        {
            yield return intervalTime;

            intervalTime = stepInterval switch
            {
                DateInterval.Minute => intervalTime.AddMinutes(1),
                DateInterval.Hour => intervalTime.AddHours(1),
                DateInterval.Day => intervalTime.AddDays(1),
                _ => throw new ArgumentOutOfRangeException(nameof(stepInterval), stepInterval, "Value not allowed!")
            };
        }
    }
}