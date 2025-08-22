using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class Schedule
{
    public string ScheduleId { get; set; } = null!;

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? ScheduleType { get; set; }

    public string? ShiftId { get; set; }

    public string? ScheduleName { get; set; }

    public virtual ICollection<ScheduleDetail> ScheduleDetails { get; set; } = new List<ScheduleDetail>();

    public virtual Shift? Shift { get; set; }
}
