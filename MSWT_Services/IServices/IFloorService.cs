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
    public interface IFloorService
    {
        #region CRUD Category
        Task<IEnumerable<FloorResponseDTO>> GetAllFloors();
        Task<FloorResponseDTO> GetFloorById(string id);
        Task<FloorResponseDTO> CreateFloorAsync(FloorRequestDTO request);
        Task UpdateFloor(Floor floor);
        Task DeleteFloor(string id);
        #endregion
    }
}
