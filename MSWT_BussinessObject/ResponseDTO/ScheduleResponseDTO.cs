using MSWT_BussinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.ResponseDTO
{
    public class ScheduleResponseDTO
    {
        public string ScheduleId { get; set; }

        public string? ScheduleName { get; set; }

        public string? AreaId { get; set; }

        public string? AssignmentId { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public string? TrashBinId { get; set; }

        public string? RestroomId { get; set; }

        public string? ScheduleType { get; set; }

        public string? ShiftId { get; set; }

        //public virtual AreaResponseDTO? Area { get; set; }

        //public virtual Assignment? Assignment { get; set; }

        //public virtual RestroomResponseDTO? Restroom { get; set; }

        //public virtual ICollection<ScheduleDetail> ScheduleDetails { get; set; } = new List<ScheduleDetail>();

        //public virtual Shift? Shift { get; set; }

        //public virtual TrashBin? TrashBin { get; set; }

        //public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
    }
}
