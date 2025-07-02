using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class Restroom
{
    public string RestroomId { get; set; } = null!;

    public string? Description { get; set; }

    public string? AreaId { get; set; }

    public string? Status { get; set; }

    public string? RestroomNumber { get; set; }

    public virtual Area? Area { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<TrashBin> TrashBins { get; set; } = new List<TrashBin>();
}
