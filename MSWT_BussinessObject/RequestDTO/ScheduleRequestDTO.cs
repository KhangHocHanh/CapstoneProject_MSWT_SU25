using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.RequestDTO
{
    public class ScheduleRequestDTO
    {
        public string? AreaId { get; set; }

        public string? ScheduleName { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public string? TrashBinId { get; set; }

        public string? RestroomId { get; set; }

        public string? ScheduleType { get; set; }

        public string? ShiftId { get; set; }
    }
}
