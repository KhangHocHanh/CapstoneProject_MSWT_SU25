using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.IServices
{
    public interface IScheduleService
    {
        Task<ScheduleResponseDTO> CreateScheduleAsync(ScheduleRequestDTO request);
        Task DeleteSchedule(string id);
        Task<IEnumerable<ScheduleResponseDTO>> GetAllSchedule();
        Task<ScheduleResponseDTO> GetScheduleById(string id);
        Task<ScheduleResponseDTO> UpdateSchedule(string scheduleId, ScheduleRequestDTO request);
    }
}
