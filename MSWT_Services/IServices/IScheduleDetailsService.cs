using MSWT_BussinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.IServices
{
    public interface IScheduleDetailsService
    {
        Task AddSchedule(ScheduleDetail scheduleDetail);
        Task DeleteSchedule(string id);
        Task<IEnumerable<ScheduleDetail>> GetAllSchedule();
        Task<ScheduleDetail> GetScheduleById(string id);
        Task UpdateSchedule(ScheduleDetail scheduleDetail);

        Task<bool> AddWorkerToSchedule(string id, string workerId);
        Task<bool> AddSupervisorToSchedule(string id, string supervisorId);
    }
}
