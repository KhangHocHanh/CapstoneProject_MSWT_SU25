using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.RequestDTO
{
    public class ScheduleDetailsRequestDTO
    {
        //public string? ScheduleId { get; set; }

        public string? Description { get; set; }

        public DateTime? Date { get; set; }

        public string? Status { get; set; }

        //public string? SupervisorId { get; set; }

        //public string? Rating { get; set; }

        public string? WorkerGroupId { get; set; }

        //public string? BackupForUserId { get; set; }

        //public TimeOnly? EndTime { get; set; }

        public TimeOnly? StartTime { get; set; }

        //public string? IsBackup { get; set; }

        public string? GroupAssignmentId { get; set; }

        //public string? Comment { get; set; }

        public string? AreaId { get; set; }
    }
}
