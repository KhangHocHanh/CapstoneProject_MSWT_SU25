using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class Area
{
    public string AreaId { get; set; } = null!;

    public string? BuildingId { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public string? AreaName { get; set; }

    public virtual Building? Building { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

    public virtual ICollection<TrashBin> TrashBins { get; set; } = new List<TrashBin>();
}
