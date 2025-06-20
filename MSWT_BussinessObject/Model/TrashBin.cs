using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class TrashBin
{
    public string TrashBinId { get; set; } = null!;

    public string? Status { get; set; }

    public string? FloorId { get; set; }

    public string? Location { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Alert> Alerts { get; set; } = new List<Alert>();

    public virtual Floor? Floor { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<SensorBin> SensorBins { get; set; } = new List<SensorBin>();
}
