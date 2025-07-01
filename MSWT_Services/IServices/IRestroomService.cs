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
    public interface IRestroomService
    {
        #region CRUD Category
        Task<IEnumerable<RestroomResponseDTO>> GetAllRestrooms();
        Task<RestroomResponseDTO?> GetRestroomById(string id);
        Task<RestroomResponseDTO> AddRestroom(RestroomRequestDTO request);
        Task<RestroomResponseDTO> UpdateRestroom(string restroomId, RestroomRequestDTO request);
        Task DeleteRestroom(string id);
        #endregion
    }
}
