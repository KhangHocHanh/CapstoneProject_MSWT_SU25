using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class Room
{
    public string RoomId { get; set; } = null!;

    public string? Description { get; set; }

    public string? AreaId { get; set; }

    public string? Status { get; set; }

    public string? RoomNumber { get; set; }

    public string? RoomType { get; set; }

    public virtual Area? Area { get; set; }

    public virtual ICollection<TrashBin> TrashBins { get; set; } = new List<TrashBin>();
}
