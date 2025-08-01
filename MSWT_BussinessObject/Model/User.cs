using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateOnly? CreateAt { get; set; }

    public string? Status { get; set; }

    public string? Address { get; set; }

    public string? Image { get; set; }

    public string? RoleId { get; set; }

    public double? Rating { get; set; }

    public string? ReasonForLeave { get; set; }

    public string? IsAssigned { get; set; }

    public virtual ICollection<Alert> Alerts { get; set; } = new List<Alert>();

    public virtual ICollection<Leaf> LeafApprovedByNavigations { get; set; } = new List<Leaf>();

    public virtual ICollection<Leaf> LeafWorkers { get; set; } = new List<Leaf>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Request> RequestSupervisors { get; set; } = new List<Request>();

    public virtual ICollection<Request> RequestWorkers { get; set; } = new List<Request>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<ScheduleDetail> ScheduleDetailBackupForUsers { get; set; } = new List<ScheduleDetail>();

    public virtual ICollection<ScheduleDetail> ScheduleDetailSupervisors { get; set; } = new List<ScheduleDetail>();

    public virtual ICollection<ScheduleDetail> ScheduleDetailWorkers { get; set; } = new List<ScheduleDetail>();
    public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();

}
