using Microsoft.AspNetCore.Http;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;

namespace MSWT_Services.IServices
{
    public interface IScheduleDetailsService
    {
        Task<ScheduleDetailsResponseDTO> CreateScheduleDetailFromScheduleAsync(string scheduleId, ScheduleDetailsRequestDTO detailDto);
        Task DeleteSchedule(string id);
        Task<IEnumerable<ScheduleDetailsResponseDTO>> GetAllSchedule();
        Task<ScheduleDetailsResponseDTO> GetScheduleById(string id);
        Task UpdateSchedule(ScheduleDetail scheduleDetail);

        //Task<bool> AddWorkerToSchedule(string id, string workerId);
        //Task<bool> AddSupervisorToSchedule(string id, string supervisorId);
        //Task<bool> UpdateRating(string id, ScheduleDetailsUpdateRatingRequestDTO request);
        //Task<IEnumerable<ScheduleDetailsResponseDTO>> SearchScheduleDetailsByUserIdAsync(string userId);
        //Task<bool> AddAssignmentToSchedule(string id, string assignmentId);
        //Task<bool> CreateDailyRatingAsync(string userId, ScheduleDetailRatingCreateDTO dto);
        //Task UpdateScheduleDetailStatusesAsync();
        //Task<bool> UpdateScheduleDetailStatusToComplete(string scheduleDetailId, string currentUserId, IFormFile? newEvidenceImage = null);
        //Task<double?> GetAverageRatingForMonthAsync(string workerId, int year, int month);
        //Task<(int workedDays, int totalDays, double percentage)> GetWorkStatsInMonthAsync(string workerId, int month, int year);
    }
}
