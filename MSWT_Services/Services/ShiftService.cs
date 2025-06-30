using AutoMapper;
using CustomEnum = MSWT_BussinessObject.Enum;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Repositories.IRepository;
using MSWT_Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.Services
{
    public class ShiftService : IShiftService
    {
        private readonly IShiftRepository _shiftRepository;
        private readonly IMapper _mapper;
        public ShiftService(IShiftRepository shiftRepository, IMapper mapper)
        {
            _shiftRepository = shiftRepository;
            _mapper = mapper;
        }
        public async Task<ShiftResponseDTO> AddShift(ShiftRequestDTO request)
        {
            var shift = _mapper.Map<Shift>(request);
            shift.ShiftId = Guid.NewGuid().ToString();
            shift.Status = CustomEnum.Enum.ShiftStatus.IsNotDeleted.ToString();

            await _shiftRepository.AddAsync(shift);

            return _mapper.Map<ShiftResponseDTO>(shift);
        }


        public async Task DeleteShift(string id)
        {
            await _shiftRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ShiftResponseDTO>> GetAllShifts()
        {
            var shift = await _shiftRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ShiftResponseDTO>>(shift);
        }

        public async Task<ShiftResponseDTO> GetShiftById(string id)
        {
            var shift= await _shiftRepository.GetByIdAsync(id);
            return _mapper.Map<ShiftResponseDTO>(shift);
        }

        public async Task<ShiftResponseDTO> UpdateShift(string shiftId, ShiftRequestDTO request)
        {
            var existingShift = await _shiftRepository.GetByIdAsync(shiftId);
            if (existingShift == null)
                throw new Exception("Shift not found.");

            _mapper.Map(request, existingShift);
            existingShift.ShiftId = shiftId; // Ensure ID is retained
            existingShift.Status = CustomEnum.Enum.ShiftStatus.IsNotDeleted.ToString();

            await _shiftRepository.UpdateAsync(existingShift);

            return _mapper.Map<ShiftResponseDTO>(existingShift);
        }

    }
}
