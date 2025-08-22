using MSWT_BussinessObject.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.RequestDTO
{
    public class RoomRequestDTO
    {
        public string? Description { get; set; }

        public string? AreaId { get; set; }

        public string? Status { get; set; }

        public string? RoomNumber { get; set; }

        public string? RoomType { get; set; }
    }
}
