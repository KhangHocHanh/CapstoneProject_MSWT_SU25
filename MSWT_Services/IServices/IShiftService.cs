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
    public interface IShiftService
    {
        #region CRUD Category
        Task<IEnumerable<ShiftResponseDTO>> GetAllShifts();
        Task<ShiftResponseDTO> GetShiftById(string id);
        Task<ShiftResponseDTO> AddShift(ShiftRequestDTO request);
        Task<ShiftResponseDTO> UpdateShift(string shiftId, ShiftRequestDTO request);
        Task DeleteShift(string id);
        #endregion
    }
}
