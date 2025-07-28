using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;
using MSWT_Services.IServices;
using static MSWT_BussinessObject.Enum.Enum;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

namespace MSWT_Services.Services
{
    public class ShiftSwapService : IShiftSwapService
    {
        private readonly IShiftSwapRepository _repo;
        private readonly IUserRepository _userRepo;
        private readonly IScheduleDetailsRepository _scheduleRepo;
        private readonly ILogger<ShiftSwapService> _logger;
        private readonly SmartTrashBinandCleaningStaffManagementContext _context;
        private readonly IMapper _mapper;

        public ShiftSwapService(IShiftSwapRepository swapRepo, IUserRepository userRepo, IScheduleDetailsRepository scheduleRepo, ILogger<ShiftSwapService> logger, SmartTrashBinandCleaningStaffManagementContext context, IMapper mapper)
        {
            _repo = swapRepo;
            _userRepo = userRepo;
            _scheduleRepo = scheduleRepo;
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ShiftSwapRequest> RequestSwapAsync(string requesterId, ShiftSwapRequestDTO dto)
        {
            var request = new ShiftSwapRequest
            {
                SwapRequestId = Guid.NewGuid(),
                RequestDate = DateTime.UtcNow,
                RequesterId = requesterId,
                TargetUserId = dto.ToUserId,
                TargetUserPhone = dto.TargetUserPhone,
                Month = dto.Month,
                Year = dto.Year,
                Reason = dto.Reason,
                Status = "Đã gửi"
            };

            return await _repo.CreateRequestAsync(request);
        }

        public async Task<List<ShiftSwapResponseDTO>> GetUserRequestsAsync(string userId)
        {
            var requests = await _repo.GetRequestsForUserAsync(userId);
            return _mapper.Map<List<ShiftSwapResponseDTO>>(requests);
        }

        public async Task<ShiftSwapRequest?> RespondSwapAsync(string userId, ShiftSwapRespondDTO dto)
        {
            var request = await _repo.GetByIdAsync(dto.RequestId);
            if (request == null || request.TargetUserId != userId || request.Status != "Đã gửi")
                return null;

            if (!dto.IsAccepted)
            {
                request.Status = "Từ chối";
                request.ConfirmedDate = DateTime.UtcNow;
                await _repo.SaveChangesAsync();
                return request;
            }

            var startDate = new DateTime(request.Year, request.Month, 1);
            var endDate = startDate.AddMonths(1);

            var fromDetails = await _context.ScheduleDetails
                .Where(s => s.WorkerId == request.RequesterId && s.Date >= startDate && s.Date < endDate)
                .ToListAsync();

            var toDetails = await _context.ScheduleDetails
                .Where(s => s.WorkerId == request.TargetUserId && s.Date >= startDate && s.Date < endDate)
                .ToListAsync();

            foreach (var detail in fromDetails)
                detail.WorkerId = request.TargetUserId;

            foreach (var detail in toDetails)
                detail.WorkerId = request.RequesterId;

            request.Status = "Đồng ý";
            request.ConfirmedDate = DateTime.UtcNow;
            request.SwapExecuted = true;

            await _repo.SaveChangesAsync();
            return request;
        }
    }
}
