using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MSWT_BussinessObject.Model;
using MSWT_Services.Services;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

namespace MSWT_Services.IServices
{
    public interface IShiftSwapService
    {
        Task<ShiftSwapRequest> RequestSwapAsync(string requesterId, ShiftSwapRequestDTO dto);
        Task<List<ShiftSwapResponseDTO>> GetUserRequestsAsync(string userId);
        Task<ShiftSwapRequest?> RespondSwapAsync(string userId, ShiftSwapRespondDTO dto);
    }
}
