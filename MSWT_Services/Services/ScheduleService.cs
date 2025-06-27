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
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;
        public ScheduleService(IScheduleRepository scheduleRepository, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
        }
        public async Task<ScheduleResponseDTO> CreateScheduleAsync(ScheduleRequestDTO request)
        {
            var schedule = _mapper.Map<Schedule>(request);
            schedule.ScheduleId = Guid.NewGuid().ToString(); // Generate UID

            await _scheduleRepository.AddAsync(schedule);
            return _mapper.Map<ScheduleResponseDTO>(schedule);
        }

        public async Task DeleteSchedule(string id)
        {
            await _scheduleRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ScheduleResponseDTO>> GetAllSchedule()
        {
            var schedules = await _scheduleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ScheduleResponseDTO>>(schedules);
        }

        public async Task<ScheduleResponseDTO> GetScheduleById(string id)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(id);
            return _mapper.Map<ScheduleResponseDTO>(schedule);
        }

        public async Task UpdateSchedule(Schedule schedule)
        {
            await _scheduleRepository.UpdateAsync(schedule);
        }

    }
}
