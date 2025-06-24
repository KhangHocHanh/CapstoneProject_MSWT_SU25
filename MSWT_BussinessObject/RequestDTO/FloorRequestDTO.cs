using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.RequestDTO
{
    public class FloorRequestDTO
    {
        public int? NumberOfRestroom { get; set; }

        public int? NumberOfBin { get; set; }

        public string? Status { get; set; }

        public int? FloorNumber { get; set; }
    }
}
