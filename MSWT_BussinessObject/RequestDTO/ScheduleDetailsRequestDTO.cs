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
        public string? Description { get; set; }

        //public DateTime Date { get; set; }

        public string? Status { get; set; }

        //public string? SupervisorId { get; set; }

        public string? WorkerId { get; set; }

        public string? AssignmentId { get; set; }

        public string? IsBackup { get; set; }

        public string? BackupForUserId { get; set; }

        public IFormFile? EvidenceImageFile { get; set; }
    }
}
