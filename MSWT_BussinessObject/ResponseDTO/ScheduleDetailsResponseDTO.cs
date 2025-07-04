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

        public string? Rating { get; set; }

        public string? WorkerId { get; set; }

        public string? EvidenceImage { get; set; }

        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }

        public string? IsBackup { get; set; }

        public string? BackupForUserId { get; set; }

        public ScheduleResponseDTO Schedule { get; set; }
    }
}
