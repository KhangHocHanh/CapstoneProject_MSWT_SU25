using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;

namespace MSWT_Repositories.IRepository
{
    public interface IShiftSwapRepository
    {
        Task AddAsync(ShiftSwapRequest request);
        Task<List<ShiftSwapRequest>> GetRequestsByUserIdAsync(string userId);
        Task<ShiftSwapRequest?> GetByIdAsync(Guid requestId);
        Task<int> GetSwapCountInMonthAsync(string userId, int month, int year);
        Task SaveChangesAsync();
    }
}
