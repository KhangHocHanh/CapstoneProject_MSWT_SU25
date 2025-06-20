using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class ScheduleDetail
{
    public string ScheduleDetailId { get; set; } = null!;

    public string? ScheduleId { get; set; }

    public string? Description { get; set; }

    public DateOnly? Date { get; set; }

    public string? Status { get; set; }

    public string? SupervisorId { get; set; }

    public string? Rating { get; set; }

    public string? WorkerId { get; set; }

    public string? EvidenceImage { get; set; }

    public virtual Schedule? Schedule { get; set; }

    public virtual User? Supervisor { get; set; }

    public virtual User? Worker { get; set; }
}
