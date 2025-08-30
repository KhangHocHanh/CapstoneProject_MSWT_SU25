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
                .Include(sd => sd.GroupAssignment)
                .Include(sd => sd.Area)
                .Include(sd => sd.WorkerGroup)
                 .ThenInclude(wg => wg.WorkGroupMembers)
                    .ThenInclude(wgm => wgm.User)

                .FirstOrDefaultAsync(sd => sd.ScheduleDetailId == id);
        }

        async Task<IEnumerable<ScheduleDetail>> IScheduleDetailsRepository.GetAllAsync()
        {
            return await _context.ScheduleDetails
                .Include(sd => sd.Schedule)
                .Include(sd => sd.GroupAssignment)
                .Include(sd => sd.Area)
                .Include(sd => sd.WorkerGroup)
                 .ThenInclude(wg => wg.WorkGroupMembers)
                    .ThenInclude(wgm => wgm.User)
                .ToListAsync();
        }

        public async Task UpdateAsync(ScheduleDetail scheduleDetail)
        {
            _context.ScheduleDetails.Update(scheduleDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ScheduleDetail>> SearchByUserIdAsync(string userId)
        {
            return await _context.ScheduleDetails
        .Include(sd => sd.Schedule)
        .Include(sd => sd.GroupAssignment)
        .Include(sd => sd.Area)
        .Include(sd => sd.WorkerGroup)
            .ThenInclude(wg => wg.WorkGroupMembers)
                .ThenInclude(wgm => wgm.User)
        .Where(sd => sd.WorkerGroupId != null &&
                     sd.WorkerGroup.WorkGroupMembers
                         .Any(wgm => wgm.UserId == userId))
        .ToListAsync();
        }

        public async Task<IEnumerable<ScheduleDetail>> GetByUserIdAndDateAsync(string userId, DateTime date)
        {
            return await _context.ScheduleDetails
                .Include(sd => sd.Schedule)
                .Include(sd => sd.GroupAssignment)
                .Include(sd => sd.Area)
                .Include(sd => sd.WorkerGroup)
                    .ThenInclude(wg => wg.WorkGroupMembers)
                        .ThenInclude(wgm => wgm.User)
                .Where(sd => sd.Date.HasValue
                          && sd.Date.Value.Date == date.Date
                          && sd.WorkerGroupId != null
                          && sd.WorkerGroup.WorkGroupMembers.Any(wgm => wgm.UserId == userId))
                .ToListAsync();
        }

        public async Task<(IEnumerable<ScheduleDetail> Items, int TotalCount)> GetByDatePaginatedAsync(DateTime date, int pageNumber, int pageSize)
        {
            var query = _context.ScheduleDetails
                .Include(sd => sd.Schedule)
                .Include(sd => sd.GroupAssignment)
                .Include(sd => sd.Area)
                .Include(sd => sd.WorkerGroup)
                    .ThenInclude(wg => wg.WorkGroupMembers)
                        .ThenInclude(wgm => wgm.User)
                .Where(sd => sd.Date.HasValue && sd.Date.Value.Date == date.Date);

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(sd => sd.StartTime) // optional: order by start time
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<IEnumerable<ScheduleDetail>> GetByDateAsync(DateTime date)
        {
            return await _context.ScheduleDetails
                .Include(sd => sd.Schedule)
                .Include(sd => sd.GroupAssignment)
                .Include(sd => sd.Area)
                .Include(sd => sd.WorkerGroup)
                    .ThenInclude(wg => wg.WorkGroupMembers)
                        .ThenInclude(wgm => wgm.User)
                .Where(sd => sd.Date.HasValue
                          && sd.Date.Value.Date == date.Date)
                .ToListAsync();
        }

        //public async Task<ScheduleDetail?> GetByUserAndDateAsync(string userId, DateOnly targetDate)
        //{
        //    return await _context.ScheduleDetails
        //        .Include(sd => sd.Schedule)
        //        //.Include(sd => sd.Worker) // Nếu cần thông tin User
        //        .FirstOrDefaultAsync(sd =>
        //            sd.WorkerId == userId &&
        //            sd.Schedule != null &&
        //            sd.Schedule.StartDate <= targetDate &&
        //            sd.Schedule.EndDate >= targetDate);
        //}
        //public async Task<double?> GetAverageRatingForMonthAsync(string workerId, int year, int month)
        //{
        //    var today = DateTime.Today;

        //    var ratings = await _context.ScheduleDetails
        //        .Where(sd => sd.WorkerId == workerId
        //                     && sd.Date.HasValue
        //                     && sd.Date.Value.Year == year
        //                     && sd.Date.Value.Month == month
        //                     && sd.Date <= today
        //                     && !string.IsNullOrEmpty(sd.Rating))
        //        .ToListAsync(); // Truy vấn SQL trước

        //    var parsedRatings = ratings
        //        .Select(sd => double.TryParse(sd.Rating, out var r) ? (double?)r : null)
        //        .Where(r => r.HasValue)
        //        .Select(r => r.Value)
        //        .ToList();

        //    return parsedRatings.Any() ? parsedRatings.Average() : null;
        //}

        //public async Task<(int workedDays, int totalDays, double percentage)> GetWorkStatsInMonthAsync(string workerId, int month, int year)
        //{
        //    // Tổng số ngày có lịch làm
        //    var totalDays = await _context.ScheduleDetails
        //        .Where(d => d.WorkerId == workerId &&
        //                    d.Date.Value.Month == month &&
        //                    d.Date.Value.Year == year)
        //        .Select(d => d.Date.Value.Date) // lấy phần ngày (bỏ giờ)
        //        .Distinct()
        //        .CountAsync();

        //    // Số ngày đã hoàn thành
        //    var workedDays = await _context.ScheduleDetails
        //        .Where(d => d.WorkerId == workerId &&
        //                    d.Status == "Hoàn thành" &&
        //                    d.Date.Value.Month == month &&
        //                    d.Date.Value.Year == year)
        //        .Select(d => d.Date.Value.Date)
        //        .Distinct()
        //        .CountAsync();

        //    // Tính phần trăm
        //    double percentage = totalDays > 0 ? (workedDays * 100.0 / totalDays) : 0;

        //    return (workedDays, totalDays, percentage);
        //}


        public async Task<List<string>> GetDistinctScheduleDatesAsync()
        {
            var dates = await _context.ScheduleDetails
                .Where(s => s.Date.HasValue)
                .Select(s => s.Date.Value.Date)
                .Distinct()
                .OrderBy(d => d)
                .ToListAsync();   // ✅ query executed in DB

            return dates
                .Select(d => d.ToString("yyyy-MM-dd"))
                .ToList();        // ✅ convert to string list
        }

        public async Task<ScheduleDetail?> GetByWorkerAndDateAsync(string userId, DateOnly date)
        {
            return await _context.ScheduleDetails
                .Include(sd => sd.Schedule)
                .Include(sd => sd.WorkerGroup) // Nếu cần thông tin User
                .FirstOrDefaultAsync(sd =>
                    sd.WorkerGroupId == userId &&
                    sd.Schedule != null &&
                    sd.Schedule.StartDate <= date &&
                    sd.Schedule.EndDate >= date);
        }
    }
}
