using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class Alert
{
    public string AlertId { get; set; } = null!;

    public string? TrashBinId { get; set; }

    public DateTime? TimeSend { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public string? UserId { get; set; }

    public string? Status { get; set; }

    public virtual TrashBin? TrashBin { get; set; }

    public virtual User? User { get; set; }
}
