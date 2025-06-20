using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class Area
{
    public string AreaId { get; set; } = null!;

    public string? FloorId { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public string? RoomBegin { get; set; }

    public string? RoomEnd { get; set; }

    public string? AreaName { get; set; }

    public virtual Floor? Floor { get; set; }

    public virtual ICollection<Restroom> Restrooms { get; set; } = new List<Restroom>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
