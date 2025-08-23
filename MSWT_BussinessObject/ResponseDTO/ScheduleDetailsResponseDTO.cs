using MSWT_BussinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.ResponseDTO
{
    public class ScheduleDetailsResponseDTO
    {
        public string ScheduleDetailId { get; set; } = null!;

        public string? ScheduleId { get; set; }

        public string? Description { get; set; }

        public DateTime? Date { get; set; }

        public string? Status { get; set; }

        public string? SupervisorId { get; set; }

        public string? SupervisorName { get; set; }

        public string? Rating { get; set; }

        public string? WorkerGroupId { get; set; }
        public string? WorkerGroupName { get; set; }

        public string? BackupForUserId { get; set; }

        public TimeOnly? EndTime { get; set; }

        public TimeOnly? StartTime { get; set; }

        public string? IsBackup { get; set; }

        public string? GroupAssignmentId { get; set; }
        public string? GroupAssignmentName { get; set; }

        public string? Comment { get; set; }

        public string? AreaId { get; set; }

        public ScheduleResponseDTO Schedule { get; set; }
        public List<WorkGroupMemberResponse> Workers { get; set; } = new();
        public List<AssignmentResponseDTO> Assignments { get; set; } = new();

    }
}
