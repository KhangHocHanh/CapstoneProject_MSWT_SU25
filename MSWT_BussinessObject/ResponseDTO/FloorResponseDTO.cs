using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.ResponseDTO
{
    public class FloorResponseDTO
    {
        public string FloorId { get; set; } = null!;
        public int? NumberOfRestroom { get; set; }
        public int? NumberOfBin { get; set; }
        public string? Status { get; set; }
        public int? FloorNumber { get; set; }

        public List<AreaResponseDTO>? Areas { get; set; }
        public List<RestroomResponseDTO>? Restrooms { get; set; }
        //public List<TrashBinResponseDTO>? TrashBins { get; set; }
    }

}
