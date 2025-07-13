using MSWT_BussinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.ResponseDTO
{
    public class RestroomResponseDTO
    {
        public string RestroomId { get; set; } = null!;
        public string? Description { get; set; }
        public string? AreaId { get; set; }
        public string? Status { get; set; }
        public string? RestroomNumber { get; set; }
        public string? AreaName { get; set; }
        //public virtual Area? Area { get; set; }

    }
}
