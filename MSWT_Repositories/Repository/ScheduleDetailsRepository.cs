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
    public class ScheduleDetailsRepository : GenericRepository<ScheduleDetail>, IScheduleDetailsRepository
    {
        public ScheduleDetailsRepository(SmartTrashBinandCleaningStaffManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddAsync(ScheduleDetail scheduleDetail)
        {
            _context.AddAsync(scheduleDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var scheduleDetail = await _context.ScheduleDetails.FindAsync(id);
            if (scheduleDetail != null)
            {
                _context.ScheduleDetails.Remove(scheduleDetail);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ScheduleDetail> GetByIdAsync(string id)
        {
            return await _context.ScheduleDetails
                .Include(sd => sd.Schedule)
                .FirstOrDefaultAsync(sd => sd.ScheduleDetailId == id);
        }

        async Task<IEnumerable<ScheduleDetail>> IScheduleDetailsRepository.GetAllAsync()
        {
            return await _context.ScheduleDetails
                .Include(sd => sd.Schedule)
                .ToListAsync();
        }

        public async Task UpdateAsync(ScheduleDetail scheduleDetail)
        {
            _context.ScheduleDetails.Update(scheduleDetail);
            await _context.SaveChangesAsync();
        }
    }
}
