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
using static MSWT_BussinessObject.RequestDTO.RequestDTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Azure.Core;

namespace MSWT_Services.Services
{
    public class ScheduleDetailsService : IScheduleDetailsService
    {
        private readonly IScheduleDetailsRepository _scheduleDetailsRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IMapper _mapper;
        //private readonly IScheduleDetailRatingRepository _scheduleDetailRatingRepository;
        private readonly ICloudinaryService _cloudinary;
        private readonly IWorkGroupMemberService _workGroupMemberService;

        public ScheduleDetailsService(IScheduleDetailsRepository scheduleDetailsRepository, 
            IUserRepository userRepository, 
            IScheduleRepository scheduleRepository, 
            IMapper mapper, 
            //IScheduleDetailRatingRepository scheduleDetailRatingRepository, 
            IAssignmentRepository assignmentRepository, 
            ICloudinaryService cloudinary,
            IWorkGroupMemberService workGroupMemberService
            )
        {
            _scheduleDetailsRepository = scheduleDetailsRepository;
            _userRepository = userRepository;
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
            //_scheduleDetailRatingRepository = scheduleDetailRatingRepository;
            _assignmentRepository = assignmentRepository;
            _cloudinary = cloudinary;
            _workGroupMemberService = workGroupMemberService;
        }

        public async Task<ScheduleDetailsResponseDTO> CreateScheduleDetailFromScheduleAsync(
    string scheduleId, ScheduleDetailsRequestDTO detailDto)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(scheduleId)
                ?? throw new Exception("Schedule not found.");

            var (supervisorId, members) =
                await _workGroupMemberService.GetSupervisorAndMembersByWorkGroupIdAsync(detailDto.WorkerGroupId);

            if (supervisorId == null)
                throw new Exception("No supervisor (RL03) found in this work group.");

            var scheduleDetail = _mapper.Map<ScheduleDetail>(detailDto);
            scheduleDetail.ScheduleId = scheduleId;
            scheduleDetail.SupervisorId = supervisorId;

            if (scheduleDetail.StartTime.HasValue)
            {
                scheduleDetail.EndTime = scheduleDetail.StartTime.Value.AddMinutes(140);
            }

            await _scheduleDetailsRepository.AddAsync(scheduleDetail);

            // map base entity -> DTO, then enrich
            var response = _mapper.Map<ScheduleDetailsResponseDTO>(scheduleDetail);
            response.SupervisorId = supervisorId;
            response.Workers = members;   // <-- already includes FullName

            return response;
        }





        public async Task DeleteSchedule(string id)
        {
            await _scheduleDetailsRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ScheduleDetailsResponseDTO>> GetAllSchedule()
        {
            var scheduleDetails = await _scheduleDetailsRepository.GetAllAsync();

            var responses = new List<ScheduleDetailsResponseDTO>();

            foreach (var detail in scheduleDetails)
            {
                var dto = _mapper.Map<ScheduleDetailsResponseDTO>(detail);

                if (!string.IsNullOrEmpty(detail.WorkerGroupId))
                {
                    var (supervisorId, members) =
                        await _workGroupMemberService.GetSupervisorAndMembersByWorkGroupIdAsync(detail.WorkerGroupId);

                    dto.SupervisorId = supervisorId;
                    dto.Workers = members;  // <-- FullName is already populated

                    var supervisor = members.FirstOrDefault(m => m.UserId == supervisorId);
                    dto.SupervisorName = supervisor?.FullName;
                }

                responses.Add(dto);
            }

            return responses;
        }




        public async Task<ScheduleDetailsResponseDTO> GetScheduleById(string id)
        {
            var scheduleDetails = await _scheduleDetailsRepository.GetByIdAsync(id);
            var dto = _mapper.Map<ScheduleDetailsResponseDTO>(scheduleDetails);

            if (!string.IsNullOrEmpty(scheduleDetails.WorkerGroupId))
            {
                var (supervisorId, members) =
                    await _workGroupMemberService.GetSupervisorAndMembersByWorkGroupIdAsync(scheduleDetails.WorkerGroupId);
                dto.SupervisorId = supervisorId;
                dto.Workers = members;
                var supervisor = members.FirstOrDefault(m => m.UserId == supervisorId);
                dto.SupervisorName = supervisor?.FullName;
            }

            return dto;
        }

        public async Task UpdateSchedule(ScheduleDetail scheduleDetail)
        {
            await _scheduleDetailsRepository.UpdateAsync(scheduleDetail);
        }

        //public async Task<bool> AddWorkerToSchedule(string id, string workerId)
        //{
        //    try
        //    {
        //        var scheduleDetail = await _scheduleDetailsRepository.GetByIdAsync(id);
        //        if (scheduleDetail == null)
        //            throw new Exception("ScheduleDetail not found.");

        //        var worker = await _userRepository.GetByIdAsync(workerId);
        //        if (worker == null)
        //            throw new Exception("Worker not found.");

        //        scheduleDetail.Worker = worker;

        //        await _scheduleDetailsRepository.UpdateAsync(scheduleDetail);
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception($"Error updating schedule detail : {e.Message}", e);
        //    }
        //}

        //public async Task<bool> AddSupervisorToSchedule(string id, string supervisorId)
        //{
        //    try
        //    {
        //        var scheduleDetail = await _scheduleDetailsRepository.GetByIdAsync(id);
        //        if (scheduleDetail == null)
        //            throw new Exception("ScheduleDetail not found.");

        //        var supervisor = await _userRepository.GetByIdAsync(supervisorId);
        //        if (supervisor == null)
        //            throw new Exception("Supervisor not found.");

        //        scheduleDetail.Supervisor = supervisor;

        //        await _scheduleDetailsRepository.UpdateAsync(scheduleDetail);
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception($"Error updating schedule detail : {e.Message}", e);
        //    }
        //}

        //public async Task<bool> AddAssignmentToSchedule(string id, string assignmentId)
        //{
        //    try
        //    {
        //        var scheduleDetail = await _scheduleDetailsRepository.GetByIdAsync(id);
        //        if (scheduleDetail == null)
        //            throw new Exception("ScheduleDetail not found.");

        //        var assignment = await _assignmentRepository.GetByIdAsync(assignmentId);
        //        if (assignment == null)
        //            throw new Exception("Assignment not found.");

        //        scheduleDetail.Assignment = assignment;

        //        await _scheduleDetailsRepository.UpdateAsync(scheduleDetail);
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception($"Error updating schedule detail : {e.Message}", e);
        //    }
        //}


        public async Task<bool> UpdateRating(string id, ScheduleDetailsUpdateRatingRequestDTO request)
        {
            try
            {
                var scheduleDetail = await _scheduleDetailsRepository.GetByIdAsync(id);
                if (scheduleDetail == null)
                    throw new Exception("ScheduleDetail not found.");

                scheduleDetail.Rating = request.Rating.ToString().TrimEnd();
                scheduleDetail.Comment = request.Comment;

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

        //public async Task<bool> CreateDailyRatingAsync(string userId, ScheduleDetailRatingCreateDTO dto)
        //{
        //    var scheduleDetail = await _scheduleDetailsRepository.GetByIdAsync(dto.ScheduleDetailId);
        //    if (scheduleDetail == null)
        //        throw new Exception("ScheduleDetail not found.");

        //    var today = DateTime.UtcNow.Date;

        //    var existingRating = await _scheduleDetailRatingRepository
        //        .GetByScheduleDetailAndDateAsync(dto.ScheduleDetailId, today);

        //    if (existingRating != null)
        //        throw new Exception("This ScheduleDetail has already been rated today.");

        //    var rating = new ScheduleDetailRating
        //    {
        //        ScheduleDetailRatingId = Guid.NewGuid().ToString(),
        //        ScheduleDetailId = dto.ScheduleDetailId,
        //        RatedByUserId = userId,
        //        RatingValue = dto.RatingValue,
        //        Comment = dto.Comment,
        //        RatedAt = DateTime.UtcNow,
        //        RatingDate = today
        //    };

        //    await _scheduleDetailRatingRepository.CreateAsync(rating);
        //    return true;
        //}

        public async Task UpdateScheduleDetailStatusesAsync()
        {
            var allScheduleDetails = await _scheduleDetailsRepository.GetAllAsync();
            var now = DateTime.Now;

            foreach (var detail in allScheduleDetails)
            {
                if (detail.Status == "Sắp tới" &&
                    detail.Date.HasValue &&
                    detail.Date.Value <= now)
                {
                    detail.Status = "Đang làm";
                    await _scheduleDetailsRepository.UpdateAsync(detail);
                }

                else if (detail.Status == "Đang làm" &&
                         detail.Date.HasValue &&
                         now >= detail.Date.Value.AddDays(1) &&
                         detail.Status != "Hoàn thành")
                {
                    detail.Status = "Chưa hoàn thành";
                    await _scheduleDetailsRepository.UpdateAsync(detail);
                }
            }
        }


        //public async Task<bool> UpdateScheduleDetailStatusToComplete(string scheduleDetailId, string currentUserId, IFormFile? newEvidenceImage = null)
        //{
        //    var detail = await _scheduleDetailsRepository.GetByIdAsync(scheduleDetailId);
        //    if (detail == null)
        //        throw new Exception("ScheduleDetail không tồn tại.");

        //    if (detail.Status == "Sắp tới")
        //        throw new Exception("Không thể hoàn thành công việc khi nó chưa bắt đầu.");

        //    if (detail.WorkerId != currentUserId)
        //        throw new Exception("Bạn không có quyền cập nhật công việc này.");

        //    if (newEvidenceImage != null)
        //    {
        //        var uploadedUrl = await _cloudinary.UploadFile(newEvidenceImage);
        //        detail.EvidenceImage = uploadedUrl;
        //    }

        //    detail.Status = "Hoàn thành";
        //    await _scheduleDetailsRepository.UpdateAsync(detail);

        //    return true;
        //}
        //public Task<double?> GetAverageRatingForMonthAsync(string workerId, int year, int month)
        //{
        //    return _scheduleDetailsRepository.GetAverageRatingForMonthAsync(workerId, year, month);
        //}
        //public async Task<(int workedDays, int totalDays, double percentage)> GetWorkStatsInMonthAsync(string workerId, int month, int year)
        //{
        //    return await _scheduleDetailsRepository.GetWorkStatsInMonthAsync(workerId, month, year);
        //}

    }
}
