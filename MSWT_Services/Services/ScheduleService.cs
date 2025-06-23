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
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        public ScheduleService(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }
        public async Task AddSchedule(Schedule schedule)
        {
            await _scheduleRepository.AddAsync(schedule);
        }

        public async Task DeleteSchedule(string id)
        {
            await _scheduleRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Schedule>> GetAllSchedule()
        {
            return await _scheduleRepository.GetAllAsync();
        }

        public async Task<Schedule> GetScheduleById(string id)
        {
            return await _scheduleRepository.GetByIdAsync(id);
        }

        public async Task UpdateSchedule(Schedule schedule)
        {
            await _scheduleRepository.UpdateAsync(schedule);
        }

    }
}
