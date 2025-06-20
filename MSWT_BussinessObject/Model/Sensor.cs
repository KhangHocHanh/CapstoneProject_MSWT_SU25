using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class Sensor
{
    public string SensorId { get; set; } = null!;

    public string? SensorName { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<SensorBin> SensorBins { get; set; } = new List<SensorBin>();
}
