using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class Assignment
{
    public string AssignmentId { get; set; } = null!;

    public string? Description { get; set; }

    public string? TimesPerDay { get; set; }

    public string? Status { get; set; }

    public string? AssigmentName { get; set; }

    public virtual ICollection<Schedule> SchedulesNavigation { get; set; } = new List<Schedule>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
