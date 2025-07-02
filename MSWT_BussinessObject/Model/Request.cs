using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class Request
{
    public string RequestId { get; set; } = null!;

    public string? WorkerId { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public DateTime? RequestDate { get; set; }

    public DateTime? ResolveDate { get; set; }

    public string? Location { get; set; }

    public string? SupervisorId { get; set; }

    public string? TrashBinId { get; set; }

    public virtual User? Supervisor { get; set; }

    public virtual TrashBin? TrashBin { get; set; }

    public virtual User? Worker { get; set; }
}
