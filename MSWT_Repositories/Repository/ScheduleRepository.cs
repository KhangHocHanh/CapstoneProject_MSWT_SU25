using Microsoft.EntityFrameworkCore;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Repositories.Repository
{
    public class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(SmartTrashBinandCleaningStaffManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Schedule> GetByIdAsync(string id)
        {
            return await _context.Schedules
        .Include(s => s.Area)
            .ThenInclude(a => a.Restrooms)
        .Include(s => s.Area)
            .ThenInclude(a => a.TrashBins)
        .Include(s => s.Restroom)
        .Include(s => s.TrashBin)
        .Include(s => s.Shift)
        .Include(s => s.ScheduleDetails)
        .FirstOrDefaultAsync(s => s.ScheduleId == id);
        }

        public async Task AddAsync(Schedule schedule)
        {
            _context.AddAsync(schedule);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
                await _context.SaveChangesAsync();
            }
        }

        async Task<IEnumerable<Schedule>> IScheduleRepository.GetAllAsync()
        {
            return await _context.Schedules
        .Include(s => s.Area)
            .ThenInclude(a => a.Restrooms)
        .Include(s => s.Area)
            .ThenInclude(a => a.TrashBins)
        .Include(s => s.Restroom)
        .Include(s => s.TrashBin)
        .Include(s => s.Shift)
        .Include(s => s.ScheduleDetails)
        .ToListAsync();
        }

        public async Task UpdateAsync(Schedule schedule)
        {
            _context.Schedules.Update(schedule);
            await _context.SaveChangesAsync();
        }
    }
}
