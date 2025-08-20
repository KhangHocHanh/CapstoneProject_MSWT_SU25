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
    public interface IRoomService
    {
        #region CRUD Category
        Task<IEnumerable<RoomResponseDTO>> GetAllRooms();
        Task<RoomResponseDTO?> GetRoomById(string id);
        Task<RoomResponseDTO> AddRoom(RoomRequestDTO request);
        Task<RoomResponseDTO> UpdateRoom(string restroomId, RoomRequestDTO request);
        Task DeleteRoom(string id);
        #endregion
    }
}
