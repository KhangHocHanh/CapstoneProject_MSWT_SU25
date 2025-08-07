using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Repositories.IRepository;
using MSWT_Repositories.Repository;
using MSWT_Services.IServices;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

namespace MSWT_Services.Services
{
    public class AlertService : IAlertService
    {
        private readonly IAlertRepository _alertRepository;
        private readonly IMapper _mapper;
        public AlertService(IAlertRepository alertRepository,IMapper mapper)
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
            await _alertRepository.AddAsync(request);
        }
        public async Task<IEnumerable<Alert>> GetAlertsByUser(string userId)
        {
            return await _alertRepository.GetAlertsByUserId(userId);
        }
        public async Task UpdateAlertStatusAsync(string alertId)
        {
            var alert = await _alertRepository.GetByIdAsync(alertId);
            if (alert != null)
            {
                alert.Status = "Đã xử lý"; 
                await _alertRepository.UpdateAsync(alert);
            }
        }
    }
}
