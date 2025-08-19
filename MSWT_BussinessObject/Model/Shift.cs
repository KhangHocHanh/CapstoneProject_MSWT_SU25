using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class Shift
{
    public string ShiftId { get; set; } = null!;

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public string? Status { get; set; }

    public string? ShiftName { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
