using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.IServices
{
    public interface IAreaService
    {
        Task<AreaResponseDTO> CreateAreaAsync(AreaRequestDTO request);
        Task DeleteArea(string id);
        Task<IEnumerable<Area>> GetAllAreas();
        Task<Area> GetAreaById(string id);
        Task UpdateArea(Area area);
    }
}
