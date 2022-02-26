using System;

namespace Vipl.Base.Extensions;

/// <summary>
/// Time period.
/// </summary>
public class DateTimeRange
{
    /// <summary>
    /// Starting time of this period.
    /// </summary>
    public DateTime StartDate { get; set; }
    /// <summary>
    /// Ending time of this period.
    /// </summary>
    public DateTime EndDate { get; set; }
}