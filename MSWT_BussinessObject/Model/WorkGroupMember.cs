using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class WorkGroupMember
{
    public string WorkGroupMemberId { get; set; } = null!;

    public string? WorkGroupId { get; set; }

    public string? UserId { get; set; }

    public string? RoleId { get; set; }

    public DateTime? JoinedAt { get; set; }

    public DateTime? LeftAt { get; set; }

    public virtual User? User { get; set; }

    public virtual WorkerGroup? WorkGroup { get; set; }
}
