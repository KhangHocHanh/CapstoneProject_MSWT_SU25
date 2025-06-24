using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.ResponseDTO
{
    public class AreaResponseDTO
    {
        public string AreaId { get; set; } = null!;

        public string? FloorId { get; set; }

        public string? Description { get; set; }

        public string? Status { get; set; }

        public string? RoomBegin { get; set; }

        public string? RoomEnd { get; set; }

        public string? AreaName { get; set; }

        public FloorResponseDTO? Floor { get; set; }
    }
}
