using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class Floor
{
    public string FloorId { get; set; } = null!;

    public int? NumberOfRestroom { get; set; }

    public int? NumberOfBin { get; set; }

    public string? Status { get; set; }

    public int? FloorNumber { get; set; }

    public virtual ICollection<Area> Areas { get; set; } = new List<Area>();
}
