using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;

namespace MSWT_Services.IServices
{
    public interface IShiftSwapService
    {
        Task<bool> RequestSwapAsync(string requesterId, DateOnly requesterDate, string targetPhone, DateOnly targetDate);
        Task<bool> RespondToSwapAsync(Guid requestId, bool isAccepted, string? reason = null);
        Task<List<ShiftSwapRequest>> GetMySwapRequestsAsync(string userId);
    }
}
