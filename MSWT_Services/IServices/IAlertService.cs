using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;

namespace MSWT_Services.IServices
{
    public interface IAlertService 
    {
        #region CRUD Category
        Task<IEnumerable<Alert>> GetAllAlerts();
        Task<Alert> GetAlertById(string id);
        //Task<FloorResponseDTO> CreateFloorAsync(FloorRequestDTO request);
        //Task<bool> UpdateAlert(string id, FloorRequestDTO request);
        Task DeleteAlert(string id);
        #endregion

    }
}
