using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class AssignmentSchedule
{
    public string AssignmentScheduleId { get; set; } = null!;

    public string? AssignmentId { get; set; }

    public string? ScheduleId { get; set; }

    public virtual Assignment? Assignment { get; set; }

    public virtual Schedule? Schedule { get; set; }
}
