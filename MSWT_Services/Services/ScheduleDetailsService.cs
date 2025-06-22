using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;
using MSWT_Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.Services
{
    public class ScheduleDetailsService : IScheduleDetailsService
    {
        private readonly IScheduleDetailsRepository _scheduleDetailsRepository;
        public ScheduleDetailsService(IScheduleDetailsRepository scheduleDetailsRepository)
        {
            _scheduleDetailsRepository = scheduleDetailsRepository;
        }
        public async Task AddSchedule(ScheduleDetail scheduleDetail)
        {
            await _scheduleDetailsRepository.AddAsync(scheduleDetail);
        }

        public async Task DeleteSchedule(string id)
        {
            await _scheduleDetailsRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ScheduleDetail>> GetAllSchedule()
        {
            return await _scheduleDetailsRepository.GetAllAsync();
        }

        public async Task<ScheduleDetail> GetScheduleById(string id)
        {
            return await _scheduleDetailsRepository.GetByIdAsync(id);
        }

        public async Task UpdateSchedule(ScheduleDetail scheduleDetail)
        {
            await _scheduleDetailsRepository.UpdateAsync(scheduleDetail);
        }
    }
}
