using AutoMapper;
using MSWT_BussinessObject.Model;
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
        public async Task AddShift(Shift shift)
        {
            await _shiftRepository.AddAsync(shift);
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

        public async Task UpdateShift(Shift shift)
        {
            await _shiftRepository.UpdateAsync(shift);
        }
    }
}
