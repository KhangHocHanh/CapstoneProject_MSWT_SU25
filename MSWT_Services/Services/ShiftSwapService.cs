using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;
using MSWT_Services.IServices;
using static MSWT_BussinessObject.Enum.Enum;

namespace MSWT_Services.Services
{
    public class ShiftSwapService : IShiftSwapService
    {
        private readonly IShiftSwapRepository _swapRepo;
        private readonly IUserRepository _userRepo;
        private readonly IScheduleDetailsRepository _scheduleRepo;

        public ShiftSwapService(IShiftSwapRepository swapRepo, IUserRepository userRepo, IScheduleDetailsRepository scheduleRepo)
        {
            _swapRepo = swapRepo;
            _userRepo = userRepo;
            _scheduleRepo = scheduleRepo;
        }

        public async Task<List<ShiftSwapRequest>> GetMySwapRequestsAsync(string userId)
        {
            return await _swapRepo.GetRequestsByUserIdAsync(userId);
        }

        public async Task<bool> RequestSwapAsync(string requesterId, DateOnly requesterDate, string targetPhone, DateOnly targetDate)
        {
            var requester = await _userRepo.GetByIdAsync(requesterId);
            var targetUser = await _userRepo.GetByPhoneAsync(targetPhone);

            if (requester == null || targetUser == null) return false;
            if (!Enum.TryParse<UserStatusEnum>(requester.Status, out var requesterStatus) ||
                !Enum.TryParse<UserStatusEnum>(targetUser.Status, out var targetStatus)) return false;
            if (requesterStatus != UserStatusEnum.DaCoLich || targetStatus != UserStatusEnum.DaCoLich) return false;

            var requesterSchedule = await _scheduleRepo.GetByUserAndDateAsync(requesterId, requesterDate);
            var targetSchedule = await _scheduleRepo.GetByUserAndDateAsync(targetUser.UserId, targetDate);
            if (requesterSchedule == null || targetSchedule == null) return false;

            int swapCount = await _swapRepo.GetSwapCountInMonthAsync(targetUser.UserId, targetDate.Month, targetDate.Year);
            if (swapCount >= 2) return false;

            var request = new ShiftSwapRequest
            {
                SwapRequestId = Guid.NewGuid(),
                RequestDate = DateTime.Now,
                RequesterId = requesterId,
                TargetUserId = targetUser.UserId,
                TargetUserPhone = targetPhone,
                RequesterScheduleDetailId = requesterSchedule.ScheduleDetailId,
                TargetScheduleDetailId = targetSchedule.ScheduleDetailId,
                Status = "Pending",
                Month = targetDate.Month,
                Year = targetDate.Year
            };

            await _swapRepo.AddAsync(request);
            await _swapRepo.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RespondToSwapAsync(Guid requestId, bool isAccepted, string? reason = null)
        {
            var request = await _swapRepo.GetByIdAsync(requestId);
            if (request == null || request.Status != "Đã gửi") return false;

            request.Status = isAccepted ? "Đồng ý" : "Từ chối";
            request.ConfirmedDate = DateTime.Now;
            request.Reason = reason;

            if (isAccepted)
            {
                // Hoán đổi ScheduleDetail
                var requesterSchedule = request.RequesterScheduleDetail;
                var targetSchedule = request.TargetScheduleDetail;

                // Hoán đổi UserId
                var temp = requesterSchedule.WorkerId;
                requesterSchedule.WorkerId = targetSchedule.WorkerId;
                targetSchedule.WorkerId = temp;

                request.SwapExecuted = true;
            }

            await _swapRepo.SaveChangesAsync();
            return true;
        }
    }
}
