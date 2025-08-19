using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class ScheduleDetail
{
    public string ScheduleDetailId { get; set; } = null!;

    public string? ScheduleId { get; set; }

    public string? Description { get; set; }

    public DateTime? Date { get; set; }

    public string? Status { get; set; }

    public string? SupervisorId { get; set; }

    public string? Rating { get; set; }

    public string? WorkerGroupId { get; set; }

    public string? BackupForUserId { get; set; }

    public TimeOnly? EndTime { get; set; }

    public TimeOnly? StartTime { get; set; }

    public string? IsBackup { get; set; }

    public string? GroupAssignmentId { get; set; }

    public string? Comment { get; set; }

    public virtual User? BackupForUser { get; set; }

    public virtual GroupAssignment? GroupAssignment { get; set; }

    public virtual Schedule? Schedule { get; set; }

    public virtual WorkerGroup? WorkerGroup { get; set; }
}
