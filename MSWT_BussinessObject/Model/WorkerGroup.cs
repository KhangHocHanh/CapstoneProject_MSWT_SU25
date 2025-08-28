using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class WorkerGroup
{
    public string WorkerGroupId { get; set; } = null!;

    public string? WorkerGroupName { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<ScheduleDetail> ScheduleDetails { get; set; } = new List<ScheduleDetail>();

    public virtual ICollection<WorkGroupMember> WorkGroupMembers { get; set; } = new List<WorkGroupMember>();
    public virtual ICollection<Alert> Alerts { get; set; } = new List<Alert>();
}
