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
    public interface IBuildingService
    {
        #region CRUD Category
        Task<IEnumerable<BuildingResponseDTO>> GetAllBuildings();
        Task<BuildingResponseDTO> GetBuildingById(string id);
        Task<BuildingResponseDTO> CreateBuildingAsync(BuildingRequestDTO request);
        Task<bool> UpdateBuilding(string id, BuildingRequestDTO request);
        Task DeleteBuilding(string id);
        #endregion
    }
}
