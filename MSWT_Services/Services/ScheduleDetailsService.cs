using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;
using MSWT_Repositories.Repository;
using MSWT_Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.Services
{
    public class ScheduleDetailsService : IScheduleDetailsService
    {
        private readonly IScheduleDetailsRepository _scheduleDetailsRepository;
        private readonly IUserRepository _userRepository;

        public ScheduleDetailsService(IScheduleDetailsRepository scheduleDetailsRepository, IUserRepository userRepository)
        {
            _scheduleDetailsRepository = scheduleDetailsRepository;
            _userRepository = userRepository;
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

        public async Task<bool> AddWorkerToSchedule(string id, string workerId)
        {
            try
            {
                var scheduleDetail = await _scheduleDetailsRepository.GetByIdAsync(id);
                if (scheduleDetail == null)
                    throw new Exception("ScheduleDetail not found.");

                var worker = await _userRepository.GetByIdAsync(workerId);
                if (worker == null)
                    throw new Exception("Worker not found.");

                scheduleDetail.Worker = worker;

                await _scheduleDetailsRepository.UpdateAsync(scheduleDetail);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"Error updating schedule detail : {e.Message}", e);
            }
        }

        public async Task<bool> AddSupervisorToSchedule(string id, string supervisorId)
        {
            try
            {
                var scheduleDetail = await _scheduleDetailsRepository.GetByIdAsync(id);
                if (scheduleDetail == null)
                    throw new Exception("ScheduleDetail not found.");

                var supervisor = await _userRepository.GetByIdAsync(supervisorId);
                if (supervisor == null)
                    throw new Exception("Supervisor not found.");

                scheduleDetail.Supervisor = supervisor;

                await _scheduleDetailsRepository.UpdateAsync(scheduleDetail);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"Error updating schedule detail : {e.Message}", e);
            }
        }
    }
}
