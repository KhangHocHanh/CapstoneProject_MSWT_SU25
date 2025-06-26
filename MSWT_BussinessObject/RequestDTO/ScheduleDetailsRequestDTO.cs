using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.RequestDTO
{
    public class ScheduleDetailsRequestDTO
    {
        public string? Description { get; set; }

        public DateOnly? Date { get; set; }

        public string? SupervisorId { get; set; }

        public string? Rating { get; set; }

        public string? WorkerId { get; set; }

        public string? EvidenceImage { get; set; }

        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }

        public string? IsBackup { get; set; }

        public string? BackupForUserId { get; set; }
    }
}
