using MSWT_BussinessObject.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.RequestDTO
{
    public class RestroomRequestDTO
    {
        public string? Description { get; set; }

        public string? AreaId { get; set; }

        public string? RestroomNumber { get; set; }
    }
}
