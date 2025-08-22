using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using static MSWT_BussinessObject.Enum.Enum;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Repositories.IRepository;
using MSWT_Repositories.Repository;
using MSWT_Services.IServices;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;
using MSWT_BussinessObject.Enum;

namespace MSWT_Services.Services
{
    public class AlertService : IAlertService
    {
        private readonly IAlertRepository _alertRepository;
        private readonly IMapper _mapper;
        public AlertService(IAlertRepository alertRepository, IMapper mapper)
        {
            _alertRepository = alertRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Alert>> GetAllAlerts()
        {
            return await _alertRepository.GetAllAsync();
        }
        public async Task<Alert> GetAlertById(string id)
        {
            return await _alertRepository.GetByIdAsync(id);
        }
        public async Task DeleteAlert(string id)
        {
            await _alertRepository.DeleteAsync(id);
        }

        public async Task CreateAlertAsync(Alert request)
        {
            //await _alertRepository.AddAsync(request);
            // Lấy UserId quản lý thùng rác tại thời điểm gửi alert
            var userId = await _alertRepository.GetUserIdForTrashBinAtTimeAsync(
                request.TrashBinId, (DateTime)request.TimeSend
            );

            request.UserId = userId; // Nếu null thì vẫn giữ null
            await _alertRepository.AddAsync(request);
        }
        public async Task<IEnumerable<Alert>> GetAlertsByUser(string userId)
        {
            return await _alertRepository.GetAlertsByUserId(userId);
        }
        public async Task UpdateAlertStatusAsync(string alertId)
        {
            var alert = await _alertRepository.GetByIdAsync(alertId);
            if (alert == null) throw new Exception("Alert not found.");
            alert.Status = AlertStatus.DaXuLy.ToDisplayString();
            alert.ResolvedAt = TimeHelper.GetNowInVietnamTime(); // Cập nhật thời gian giải quyết
            await _alertRepository.UpdateAsync(alert);

        }
        public async Task<Alert?> GetAlertByTrashBinIdAsync(string trashBinId, string status)
        {
            return await _alertRepository
                .GetAll()
                .FirstOrDefaultAsync(a => a.TrashBinId == trashBinId && a.Status == status);
        }
        public async Task<IEnumerable<AlertTrashBinDTO>> GetAllAlertsByUserIdAsync(string userId)
        {
            var alerts = await _alertRepository.GetAlertsByUserId(userId);
            return _mapper.Map<IEnumerable<AlertTrashBinDTO>>(alerts);
        }
    }
}
