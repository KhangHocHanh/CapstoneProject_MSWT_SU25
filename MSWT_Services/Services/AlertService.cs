using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;
using MSWT_Services.IServices;

namespace MSWT_Services.Services
{
    public class AlertService : IAlertService
    {
        private readonly IAlertRepository _alertRepository;
        public AlertService(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository;
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
    }
}
