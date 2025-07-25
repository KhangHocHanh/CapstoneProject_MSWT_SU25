using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;

namespace MSWT_Repositories.Repository
{
    public class ShiftSwapRepository : IShiftSwapRepository
    {
        private readonly SmartTrashBinandCleaningStaffManagementContext _context;

        public ShiftSwapRepository(SmartTrashBinandCleaningStaffManagementContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ShiftSwapRequest request)
        {
            await _context.ShiftSwapRequests.AddAsync(request);
        }

        public async Task<ShiftSwapRequest?> GetByIdAsync(Guid requestId)
        {
            return await _context.ShiftSwapRequests
                .Include(x => x.Requester)
                .Include(x => x.TargetUser)
                .Include(x => x.RequesterScheduleDetail)
                .Include(x => x.TargetScheduleDetail)
                .FirstOrDefaultAsync(x => x.SwapRequestId == requestId);
        }

        public async Task<List<ShiftSwapRequest>> GetRequestsByUserIdAsync(string userId)
        {
            return await _context.ShiftSwapRequests
                .Where(x => x.RequesterId == userId || x.TargetUserId == userId)
                .ToListAsync();
        }

        public async Task<int> GetSwapCountInMonthAsync(string userId, int month, int year)
        {
            return await _context.ShiftSwapRequests
                .Where(x => x.TargetUserId == userId && x.Month == month && x.Year == year && x.SwapExecuted)
                .CountAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
