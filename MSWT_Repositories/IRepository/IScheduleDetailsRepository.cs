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
        Task<ScheduleDetail?> GetByUserAndDateAsync(string userId, DateOnly targetDate);

        #endregion
    }
}
