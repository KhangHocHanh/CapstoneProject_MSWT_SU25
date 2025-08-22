using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class GroupAssignment
{
    public string GroupAssignmentId { get; set; } = null!;

    public string? AssignmentGroupName { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<ScheduleDetail> ScheduleDetails { get; set; } = new List<ScheduleDetail>();
}
