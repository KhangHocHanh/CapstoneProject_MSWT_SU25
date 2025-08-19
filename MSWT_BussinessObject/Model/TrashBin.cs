using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class TrashBin
{
    public string TrashBinId { get; set; } = null!;

    public string? Status { get; set; }

    public string? AreaId { get; set; }

    public string? Location { get; set; }

    public string? Image { get; set; }

    public string? RestroomId { get; set; }

    public virtual ICollection<Alert> Alerts { get; set; } = new List<Alert>();

    public virtual Area? Area { get; set; }

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual Room? Restroom { get; set; }

    public virtual ICollection<SensorBin> SensorBins { get; set; } = new List<SensorBin>();
}
