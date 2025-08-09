using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;

namespace MSWT_Repositories.Repository
{
    public class AlertRepository : GenericRepository<Alert>, IAlertRepository
    {
        public AlertRepository(SmartTrashBinandCleaningStaffManagementContext context) : base(context)
        {
            _context = context;
        }
        public async Task AddAsync(Alert alert)   
        {
            _context.AddAsync(alert);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var alert = await _context.Alerts.FindAsync(id);
            if (alert != null)
            {
                _context.Remove(alert);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Alert> GetByIdAsync(string id)
        {
            return await _context.Alerts
        .Include(a => a.User)
        .Include(a => a.TrashBin)
        .FirstOrDefaultAsync(a => a.AlertId == id);
        }

        async Task<IEnumerable<Alert>> IAlertRepository.GetAllAsync()
        {
            return await _context.Alerts
                .Include(a => a.User)
                .Include(a => a.TrashBin)
                .ToListAsync();
        }

        async Task IAlertRepository.UpdateAsync(Alert alert)
        {
            _context.Alerts.Update(alert);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Alert>> GetAlertsByUserId(string userId)
        { 
            return await _context.Alerts
                .Where(l => l.UserId == userId)
                .OrderByDescending(l => l.TimeSend)
                .ToListAsync();
        }
        public async Task<string?> GetUserIdForTrashBinAtTimeAsync(string trashBinId, DateTime alertTime)
        {
            var alertDateOnly = DateOnly.FromDateTime(alertTime);

            return await (
                from s in _context.Schedules
                join sd in _context.ScheduleDetails on s.ScheduleId equals sd.ScheduleId
                where s.TrashBinId == trashBinId
                      && s.StartDate.HasValue && s.EndDate.HasValue
                      && s.StartDate.Value <= alertDateOnly
                      && s.EndDate.Value >= alertDateOnly
                select sd.WorkerId
            ).FirstOrDefaultAsync();
        }



    }
}
