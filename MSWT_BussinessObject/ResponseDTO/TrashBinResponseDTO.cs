using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.ResponseDTO
{
    public class TrashBinResponseDTO
    {
        public string TrashBinId { get; set; }

        public string? Status { get; set; }

        public string? AreaId { get; set; }

        public string? Location { get; set; }

        public string? Image { get; set; }
    }
}
