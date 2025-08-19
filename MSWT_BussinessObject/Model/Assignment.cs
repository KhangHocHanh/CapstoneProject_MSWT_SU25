using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class Assignment
{
    public string AssignmentId { get; set; } = null!;

    public string? Description { get; set; }

    public string? Status { get; set; }

    public string? AssigmentName { get; set; }

    public string? GroupAssignmentId { get; set; }

    public virtual GroupAssignment? GroupAssignment { get; set; }
}
