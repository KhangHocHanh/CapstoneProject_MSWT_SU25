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

        public string? BuildingId { get; set; }

        public string? Description { get; set; }

        public string? Status { get; set; }

        public string? AreaName { get; set; }

        public List<RoomResponseDTO>? Rooms { get; set; }
    }
}
