using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

namespace MSWT_Repositories.IRepository
{
    public interface IShiftSwapRepository
    {
        Task<ShiftSwapRequest> CreateRequestAsync(ShiftSwapRequest request);
        Task<List<ShiftSwapResponseDTO>> GetRequestsForUserAsync(string userId);
        Task<ShiftSwapRequest?> GetByIdAsync(Guid id);
        Task SaveChangesAsync();
    }
}
