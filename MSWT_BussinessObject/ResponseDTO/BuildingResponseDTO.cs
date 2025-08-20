using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.ResponseDTO
{
    public class BuildingResponseDTO
    {
        public string BuildingId { get; set; } = null!;

        public string? BuildingName { get; set; }

        public string? Description { get; set; }

        public List<AreaResponseDTO>? Areas { get; set; }

        //public List<RestroomResponseDTO>? Restrooms { get; set; }

        //public List<TrashBinResponseDTO>? TrashBins { get; set; }
    }

}
