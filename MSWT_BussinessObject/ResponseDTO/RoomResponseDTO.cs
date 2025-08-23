using MSWT_BussinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.ResponseDTO
{
    public class RoomResponseDTO
    {
        public string RoomId { get; set; } = null!;

        public string? Description { get; set; }

        public string? AreaId { get; set; }

        public string? Status { get; set; }

        public string? RoomNumber { get; set; }

        public string? RoomType { get; set; }
        public string? AreaName { get; set; }

        //public virtual Area? Area { get; set; }

        //public virtual ICollection<TrashBin> TrashBins { get; set; } = new List<TrashBin>();

    }
}
