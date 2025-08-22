using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class Building
{
    public string BuildingId { get; set; } = null!;

    public string? BuildingName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Area> Areas { get; set; } = new List<Area>();
}
