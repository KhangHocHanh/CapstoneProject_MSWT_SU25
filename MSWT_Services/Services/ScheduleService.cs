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
using Azure.Core;

namespace MSWT_Services.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IAreaRepository _areaRepository;
        private readonly IShiftRepository _shiftRepository;
        private readonly IMapper _mapper;
        public ScheduleService(IScheduleRepository scheduleRepository, IMapper mapper, IAreaRepository areaRepository, IShiftRepository shiftRepository)
        {
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
            _areaRepository = areaRepository;
            _shiftRepository = shiftRepository;
        }
        public async Task<ScheduleResponseDTO> CreateScheduleAsync(ScheduleRequestDTO request)
        {
            var area = await _areaRepository.GetByIdAsync(request.AreaId);
            if (area == null)
            {
                throw new Exception("Area does not exist.");
            }

            var shift = await _shiftRepository.GetByIdAsync(request.ShiftId);
            if (shift == null)
            {
                throw new Exception("Shift does not exist.");
            }

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

        public async Task<ScheduleResponseDTO> UpdateSchedule(string scheduleId, ScheduleRequestDTO request)
        {
            try
            {
                var existingSchedule = await _scheduleRepository.GetByIdAsync(scheduleId);
                if (existingSchedule == null)
                {
                    throw new Exception("Schedule not found.");
                }

                var area = await _areaRepository.GetByIdAsync(request.AreaId);
                if (area == null)
                {
                    throw new Exception("Area does not exist.");
                }

                var shift = await _shiftRepository.GetByIdAsync(request.ShiftId);
                if (shift == null)
                {
                    throw new Exception("Shift does not exist.");
                }

                _mapper.Map(request, existingSchedule);
                await _scheduleRepository.UpdateAsync(existingSchedule);

                return _mapper.Map<ScheduleResponseDTO>(existingSchedule);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to update Schedule: {e.Message}");
            }
        }

    }
}
