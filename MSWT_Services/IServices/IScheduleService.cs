using MSWT_BussinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.IServices
{
    public interface IScheduleService
    {
        Task AddSchedule(Schedule schedule);
        Task DeleteSchedule(string id);
        Task<IEnumerable<Schedule>> GetAllSchedule();
        Task<Schedule> GetScheduleById(string id);
        Task UpdateSchedule(Schedule schedule);
    }
}
