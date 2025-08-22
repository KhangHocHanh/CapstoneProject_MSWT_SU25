using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class AttendanceRecord
{
    public string Id { get; set; } = null!;

    public string? EmployeeId { get; set; }

    public DateOnly? AttendanceDate { get; set; }

    public DateTime? CheckInTime { get; set; }

    public DateTime? CheckOutTime { get; set; }

    public string? Status { get; set; }

    public string? Note { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? Employee { get; set; }
}
