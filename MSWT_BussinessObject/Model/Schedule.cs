using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class Schedule
{
    public string ScheduleId { get; set; } = null!;

    public string? AreaId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? TrashBinId { get; set; }

    public string? RestroomId { get; set; }

    public string? ScheduleType { get; set; }

    public string? ShiftId { get; set; }

    public string? ScheduleName { get; set; }

    public string? SupervisorId { get; set; }

    public virtual Area? Area { get; set; }

    public virtual ICollection<AssignmentSchedule> AssignmentSchedules { get; set; } = new List<AssignmentSchedule>();

    public virtual Restroom? Restroom { get; set; }

    public virtual ICollection<ScheduleDetail> ScheduleDetails { get; set; } = new List<ScheduleDetail>();

    public virtual Shift? Shift { get; set; }

    public virtual TrashBin? TrashBin { get; set; }

    public virtual User? Supervisor { get; set; }
}
