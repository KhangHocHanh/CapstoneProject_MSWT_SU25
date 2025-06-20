using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class Leaf
{
    public string LeaveId { get; set; } = null!;

    public string WorkerId { get; set; } = null!;

    public string LeaveType { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int TotalDays { get; set; }

    public string? Reason { get; set; }

    public DateOnly RequestDate { get; set; }

    public string ApprovalStatus { get; set; } = null!;

    public string? ApprovedBy { get; set; }

    public DateOnly? ApprovalDate { get; set; }

    public string? Note { get; set; }

    public virtual User? ApprovedByNavigation { get; set; }

    public virtual User Worker { get; set; } = null!;
}
