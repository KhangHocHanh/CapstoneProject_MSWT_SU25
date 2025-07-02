using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class Report
{
    public string ReportId { get; set; } = null!;

    public string? ReportType { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public string? ReportName { get; set; }

    public string? UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Image { get; set; }

    public string? Priority { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public virtual User? User { get; set; }
}
