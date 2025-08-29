using MSWT_BussinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Repositories.IRepository
{
    public interface IScheduleDetailsRepository
    {
        #region CRUD Category
        Task AddAsync(ScheduleDetail scheduleDetail);
        Task<ScheduleDetail> GetByIdAsync(string id);
        Task DeleteAsync(string id);
        Task UpdateAsync(ScheduleDetail scheduleDetail);
        Task<IEnumerable<ScheduleDetail>> GetAllAsync();
        Task<IEnumerable<ScheduleDetail>> SearchByUserIdAsync(string userId);
        Task<IEnumerable<ScheduleDetail>> GetByUserIdAndDateAsync(string userId, DateTime date);
        Task<(IEnumerable<ScheduleDetail> Items, int TotalCount)> GetByDatePaginatedAsync(DateTime date, int pageNumber, int pageSize);
        //Task<ScheduleDetail?> GetByUserAndDateAsync(string userId, DateOnly targetDate);
        //Task<double?> GetAverageRatingForMonthAsync(string workerId, int year, int month);
        //Task<(int workedDays, int totalDays, double percentage)> GetWorkStatsInMonthAsync(string workerId, int month, int year);
        Task<ScheduleDetail?> GetByWorkerAndDateAsync(string userId, DateOnly date);

        #endregion
    }
}
