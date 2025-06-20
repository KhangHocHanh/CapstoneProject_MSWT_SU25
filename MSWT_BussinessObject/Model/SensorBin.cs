using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class SensorBin
{
    public string SensorId { get; set; } = null!;

    public string BinId { get; set; } = null!;

    public string? Status { get; set; }

    public virtual TrashBin Bin { get; set; } = null!;

    public virtual Sensor Sensor { get; set; } = null!;
}
