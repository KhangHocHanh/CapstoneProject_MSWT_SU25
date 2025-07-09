using AutoMapper;
using CustomEnum = MSWT_BussinessObject.Enum;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
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
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ScheduleDetailsService(IScheduleDetailsRepository scheduleDetailsRepository, IUserRepository userRepository, IScheduleRepository scheduleRepository, IMapper mapper)
        {
            _scheduleDetailsRepository = scheduleDetailsRepository;
            _userRepository = userRepository;
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
        }
        public async Task<ScheduleDetailsResponseDTO> CreateScheduleDetailFromScheduleAsync(string scheduleId, ScheduleDetailsRequestDTO detailDto)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(scheduleId);
            if (schedule == null)
                throw new Exception("Schedule not found.");

            var detail = new ScheduleDetail
            {
                ScheduleDetailId = Guid.NewGuid().ToString(),
                ScheduleId = schedule.ScheduleId,
                Description = detailDto.Description,
                Date = detailDto.Date,
                Status = detailDto.Status,
                StartTime = schedule.Shift.StartTime,
                EndTime = schedule.Shift.EndTime,
                IsBackup = detailDto.IsBackup,
                BackupForUserId = detailDto.BackupForUserId
            };

            await _scheduleDetailsRepository.AddAsync(detail);
            return _mapper.Map<ScheduleDetailsResponseDTO>(detail);
        }



        public async Task DeleteSchedule(string id)
        {
            await _scheduleDetailsRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ScheduleDetailsResponseDTO>> GetAllSchedule()
        {
            var scheduleDetails = await _scheduleDetailsRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ScheduleDetailsResponseDTO>>(scheduleDetails);
        }

        public async Task<ScheduleDetailsResponseDTO> GetScheduleById(string id)
        {
            var scheduleDetails = await _scheduleDetailsRepository.GetByIdAsync(id);
            return _mapper.Map<ScheduleDetailsResponseDTO>(scheduleDetails);
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

        public async Task<bool> UpdateRating(string id, string rating)
        {
            try
            {
                var scheduleDetail = await _scheduleDetailsRepository.GetByIdAsync(id);
                if (scheduleDetail == null)
                    throw new Exception("ScheduleDetail not found.");

                scheduleDetail.Rating = rating.Trim();

                await _scheduleDetailsRepository.UpdateAsync(scheduleDetail);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"Error updating schedule detail : {e.Message}", e);
            }
        }

        public async Task<IEnumerable<ScheduleDetailsResponseDTO>> SearchScheduleDetailsByUserIdAsync(string userId)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                    throw new Exception("User not found.");

                var results = await _scheduleDetailsRepository.SearchByUserIdAsync(userId);

                if (results == null || !results.Any())
                    throw new Exception("No schedule details found for the user.");

                return _mapper.Map<IEnumerable<ScheduleDetailsResponseDTO>>(results);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching schedule details for user {userId}: {ex.Message}", ex);
            }
        }

    }
}
